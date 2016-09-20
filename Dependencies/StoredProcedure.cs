using System.Data;
using System.Collections.Generic;

namespace Dependencies
{
    public class StoredProcedure
    {
        public string Name { get; set; }
        public int DependencyDepth { get; set; }
        public int NumberOfCalls { get; set; }
        public List<StoredProcedure> Procedures { get; set; }
        public List<string> Dependencies { get; set; }

        public StoredProcedure(string procedureName)
        {
            Name = procedureName;
        }

        private void PopulateProcedureDependencies()
        {
            string command = "SELECT DISTINCT referenced_entity_name FROM sys.dm_sql_referenced_entities ('";
            command += this.Name + "', 'OBJECT') WHERE referenced_entity_name IN(select name from sys.procedures) ";

            using (DBconnector dbc = new DBconnector())
            {
                dbc.ExecuteCommand(command, ProcedureType.Read);

                foreach (DataRow row in dbc.GetResultTable().Rows)
                {
                    NumberOfCalls += 1;

                    string storedProcedure = "dbo." + row.ItemArray[0].ToString();

                    StoredProcedure sp = new StoredProcedure(storedProcedure);
                    Procedures.Add(sp);

                    DependencyDepth += 1; // Increment the depth counter by one

                    sp.LookForDependencies();

                    NumberOfCalls += sp.NumberOfCalls;
                }
            }

        }

        private void PopulateDependencies()
        {
            string command = "EXEC sp_depends '" + this.Name + "'";

            using (DBconnector dbc = new DBconnector())
            {
                dbc.ExecuteCommand(command, ProcedureType.Read);

                foreach (DataRow row in dbc.GetResultTable().Rows)
                {
                    string name = row.ItemArray[0].ToString();
                    string type = row.ItemArray[1].ToString();

                    // Skip stored procedures
                    if (type == "stored procedure")
                        continue;

                    string info = $"{type}: {name}";

                    if (!Dependencies.Contains(info))
                        Dependencies.Add(info);
                }
            }

        }

        public void LookForDependencies()
        {
            Procedures = new List<StoredProcedure>();
            Dependencies = new List<string>();

            PopulateDependencies();
            PopulateProcedureDependencies();
        }
    }
}
