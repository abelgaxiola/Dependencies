// Class used to connect to the database and execute stored procedures that either
// Create, Read, Update, or Delete table rows.  The class depends on the application's
// configuration file for the connection string.  Results are returned as either 
// a DataTable or DataSet types by using GetResultTable() or GetResults() methods.
// ExecuteStoreProcedure() method depends on a list of parameter values; the calling
// process is responsible for creating and adding values to the parameter list

using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

public enum ProcedureType
{
    Create,
    Read,
    Update,
    Delete,
    NonQuery
}

public class DBconnector : IDisposable
{
    private DataTable Table { get; set; }
    private DataSet Results { get; set; }
    private SqlConnection Connection { get; set; }
    private SqlCommand Command { get; set; }
    private string ConnectionString { get; set; }
    public ConnectionState State { get; private set; }
    public bool HasError { get; private set; }
    public int ReturnValue { get; set; }
    public List<SqlParameter> OutputParameters { get; set; }
    public string Information { get; set; }

    public DBconnector()
    {
        // First connection string is considered to be the default
        string dbSource = ConfigurationManager.ConnectionStrings[1].Name;
        Initialize(dbSource);
    }

    public DBconnector(string dbSource)
    {
        Initialize(dbSource);
    }

    private void Initialize(string dbSource)
    {
        HasError = false;
        try
        {
            SetConnectionString(dbSource);
            ConnectToDatabase();
            Table = new DataTable();
            Results = new DataSet();
        }
        catch (Exception e)
        {
            WriteToLog(e.ToString());
        }
    }

    public void CloseConnection()
    {
        Connection.Close();
        State = Connection.State;
        Information += "Connection state: " + State.ToString() + "\n";
        Dispose();
    }

    public void ExecuteCommand(string cmdText, ProcedureType procedureType)
    {
        try
        {
            Command = new SqlCommand(cmdText, Connection);
            Command.CommandType = CommandType.Text;

            Execute(procedureType);
        }
        catch (Exception exception)
        {
            WriteToLog(exception.ToString());
        }
    }

    public void ExecuteStoredProcedure(string storedProcedureName, List<SPparameter> parameters, ProcedureType procedureType, List<SqlParameter> outputParameters = null)
    {
        // Most stored procedures being called do not have output parameters that is why it is initialiased as null and not required.
        OutputParameters = outputParameters;

        try
        {
            Information += "Stored procedure name: " + storedProcedureName + "\n";
            Information += "Stored procedure type: " + procedureType.ToString() + "\n";

            Command = new SqlCommand(storedProcedureName, Connection);
            Command.CommandType = CommandType.StoredProcedure;

            AddParameters(parameters);

            Execute(procedureType);
        }
        catch (Exception exception)
        {
            WriteToLog(exception.ToString());
        }

    }

    public DataTable GetResultTable()
    {
        return Table;
    }

    public DataSet GetResults()
    {
        return Results;
    }

    private void Execute(ProcedureType procedureType)
    {
        switch (procedureType)
        {
            case ProcedureType.Delete:
                ReturnValue = Command.ExecuteNonQuery();
                break;
            case ProcedureType.Create:
                ReturnValue = Command.ExecuteNonQuery();
                break;
            case ProcedureType.Read:
                FillDataTable();
                break;
            case ProcedureType.Update:
                ReturnValue = Command.ExecuteNonQuery();
                break;
            case ProcedureType.NonQuery:
                ReturnValue = Command.ExecuteNonQuery();
                break;
        }
    }

    private void FillDataTable()
    {
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(Command);
            da.Fill(Results);

            if (Results.Tables.Count > 0)
            {
                Table = Results.Tables[0];

                // Check to see if a single row is returned.  It may be that the stored procedure returns a single value
                if (Table.Rows.Count == 1)
                    SetReturnValue();
            }
        }
        catch (Exception e)
        {
            WriteToLog(e.ToString());
        }
    }

    private void SetReturnValue()
    {
        // This method will test for single integer and boolean values and set the ReturnValue property accordingly
        DataRow row = Table.Rows[0];

        if (row.ItemArray.GetLength(0) == 1)
        {
            int intValue;
            string stringValue = row[0].ToString();

            if (stringValue.ToUpper() == "TRUE")
                stringValue = "1";

            bool goodValue = int.TryParse(stringValue, out intValue);

            if (goodValue)
                ReturnValue = intValue;
        }
    }

    private void AddParameters(List<SPparameter> parameters)
    {
        if (OutputParameters != null)
        {
            foreach (var parameter in OutputParameters)
            {
                parameter.Direction = ParameterDirection.Output;
                Command.Parameters.Add(parameter);

                Information += "Output Parameter name: " + parameter.ToString() + "\n";
            }
        }

        if (parameters != null)
        {
            foreach (var parameter in parameters)
            {
                Command.Parameters.AddWithValue(parameter.Name, parameter.Value);

                Information += "Parameter name: " + parameter.Name + "\n";
                Information += "Parameter value: " + parameter.Value + "\n";
            }
        }
    }

    private void SetConnectionString(string dbSource)
    {
        ConnectionString = ConfigurationManager.ConnectionStrings[dbSource].ToString();

        Debug.Assert(ConnectionString != null);
        if (ConnectionString == null)
        {
            WriteToLog("There was an error trying to set the connection string inside DBconnector...");
        }
    }

    private void ConnectToDatabase()
    {
        try
        {
            if (!HasError)
            {
                Connection = new SqlConnection(ConnectionString);
                Connection.Open();
                Debug.Assert(Connection.State == System.Data.ConnectionState.Open);
                State = Connection.State;
                Debug.Assert(Connection != null);
            }
        }
        catch (Exception e)
        {
            WriteToLog(e.ToString());
        }
    }

    private Boolean disposed;

    protected virtual void Dispose(Boolean disposing)
    {
        if (disposed)
            return;

        if (disposing)
        {
            try
            {
                if (Connection != null)
                    Connection.Dispose();

                if (Table != null)
                    Table.Dispose();

                if (Results != null)
                    Results.Dispose();
            }
            catch (Exception e)
            {
                WriteToLog(e.ToString());
            }
        }

        disposed = true;
    }

    public void Dispose()
    {
        this.Dispose(true);

        GC.SuppressFinalize(this);
    }

    ~DBconnector()
    {
        this.Dispose(false);
    }

    private void WriteToLog(string ExceptionMessage)
    {
        // Let the calling process know there was an error
        HasError = true;

        ExceptionMessage += "\n\nAdditional information:\n\n" + Information;

        ApplicationLog logger = new ApplicationLog(ExceptionMessage);
        logger.Write();
    }

} // End DBconnector class