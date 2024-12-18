using IniParser;
using IniParser.Model;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Process_Spawn_Monitor
{
    static class IniFileManager
    {
        public const string iniFileName = "ProcessSpawnMonitor.ini";
        public const string commentString = "#";
        public const string knownProcessList = "KnownProcessList";
        public enum OnNewDetectedUnlistedProcess
        {
            Terminate,
            TerminateOnlyKnownLeaveOthers,
            SuspendAndAsk,
            Ask,
            DoNothing
        }
        public static void AddToKnownProcessList(string processName, string processFilter)
        {
            var data = IniFileManager.LoadIniFile(IniFileManager.iniFileName);

            if (data[knownProcessList][processName] != null)
            {
                data[knownProcessList][processName] = processFilter;
            }
            else
            {
                data[knownProcessList].AddKey(processName, processFilter);
            }
            var parser = new FileIniDataParser();
            parser.WriteFile(IniFileManager.iniFileName, data);
        }
        public static string GetKeyValue(string sectionName, string keyName)
        {
            var data = IniFileManager.LoadIniFile(IniFileManager.iniFileName);
            string value = data[sectionName][keyName];
            return value;
        }
        public static bool GetKeyValueBool(string sectionName, string keyName)
        {
            try
            {
                var data = IniFileManager.LoadIniFile(IniFileManager.iniFileName);
                string value = data[sectionName][keyName];
                if (value.Equals("True"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        internal static bool CheckProcessKeyExists(string sectionName, ProcessEntry32.PROCESSENTRY32 processEntry32)
        {
            try
            {
                var data = IniFileManager.LoadIniFile(IniFileManager.iniFileName);
                string result = data[sectionName][processEntry32.szExeFile];
                var aaa = data[sectionName];
                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }
        public static IniData LoadMainIniFile()
        {
            try
            {
                string shellCommands = IniFileManager.iniFileName;
                string applicationPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                string fullPath = Path.Combine(Path.GetDirectoryName(applicationPath), shellCommands);

                var parser = new FileIniDataParser();
                parser.Parser.Configuration.CommentString = IniFileManager.commentString;
                IniData data = parser.ReadFile(fullPath);

                return data;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return null;
        }
        public static IniData LoadIniFile(string iniFileName)
        {
            try
            {
                string applicationPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                string fullPath = Path.Combine(Path.GetDirectoryName(applicationPath), iniFileName);

                var parser = new FileIniDataParser();
                parser.Parser.Configuration.CommentString = IniFileManager.commentString;
                IniData data = parser.ReadFile(fullPath);

                return data;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return null;
        }
    }
}
