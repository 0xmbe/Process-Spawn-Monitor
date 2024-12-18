using System;
using System.IO;
using System.Reflection;

namespace Process_Spawn_Monitor
{
    public class DebugFileLogger
    {
        public static string GetLogFilePath()
        {
            string exeFilename = Assembly.GetExecutingAssembly().GetName().Name;
            string logFileName = exeFilename + ".log";
            string applicationPath = AppDomain.CurrentDomain.BaseDirectory;
            string fullFileLogPath = Path.Combine(Path.GetDirectoryName(applicationPath), logFileName);
            return fullFileLogPath;
        }
        public void DebugLogToFile(string message)
        {
            try
            {
                string fullFileLogPath = GetLogFilePath();
                using (StreamWriter writer = new StreamWriter(fullFileLogPath, true))
                {
                    writer.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")} {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing to log file: " + ex.Message);

            }
        }
    }
}
