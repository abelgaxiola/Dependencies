// Class used for logging errors, exeptions or any other type of information.
// It depends on the application configuration file for the folder and name
// of the log file.  If none is provided it writes to the application's folder

using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Configuration;

public class ApplicationLog
{
    private string FileLocation { get; set; }
    private string LogMessage { get; set; }

    public ApplicationLog()
    {
        SetFileLocation();
    }

    public ApplicationLog(string message)
        : this()
    {
        LogMessage = message;
    }

    public void Write()
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(FileLocation, true))
            {
                sw.WriteLine("=========================================================");
                sw.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
                sw.WriteLine("=========================================================");
                sw.WriteLine(LogMessage);
                sw.Flush();
            }
        }
        catch (Exception ex)
        {
            WriteToEventLog(ex.ToString());
        }
    }

    public void Write(string message)
    {
        LogMessage = message;
        Write();
    }

    private void SetFileLocation()
    {
        FileLocation = string.Empty;
        try
        {
            //Get the values from the configuration file
            string logFile = ConfigurationManager.AppSettings.Get("LogFile");
            string logFilePath = ConfigurationManager.AppSettings.Get("LogFilePath");

            //Test to ensure that the file path exists
            if (Directory.Exists(logFilePath))
            {
                FileLocation = logFilePath + logFile;
            }
            else //It doesn't exist.  Write to current directory
            {
                FileLocation = AppDomain.CurrentDomain.BaseDirectory + "\\logFile.txt";
            }
        }
        catch (Exception ex)
        {
            WriteToEventLog(ex.ToString());
        }
    }

    private void WriteToEventLog(string LogMessage)
    {
        EventLog e = new EventLog();
        try
        {
            e.Source = Assembly.GetExecutingAssembly().FullName;
            e.Log = "Application";
            e.WriteEntry(LogMessage);
            e.Dispose();
        }
        catch (Exception)
        {
            if (e != null)
            {
                e.Dispose();
            }
        }
    }
}