using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Text.RegularExpressions;
using System.Diagnostics;

using static ProcessInfoLib.ProcessInfoClass;
using Process_Spawn_Monitor;
using System.Runtime.InteropServices;
using static Process_Spawn_Monitor.ProcessEntry32;

namespace ProcessInfoLib
{
    /// <summary>
    /// This is empty object for a process wa want to get the data from. 
    /// This gets properties from the process and fills them in this object.
    /// </summary>
    public class ProcessInfo
    {
        [ThisParameterColumnWidth(185)]
        public string CreationDate { get; set; }
        //public int Number { get; set; }
        [ThisParameterColumnWidth(130)]
        public string ProcessName { get; set; }
        [ThisParameterColumnWidth(60)]
        public uint PID { get; set; }
        [ThisParameterColumnWidth(60)]
        public uint PPID { get; set; }
        public string ParentProcessName { get; set; }
        [ThisParameterColumnWidth(280)]
        public string CommandLine { get; set; }

        [ThisParameterColumnWidth(280)]
        public string ExecutablePath { get; set; }
        public uint ThreadCount { get; set; }
        public uint HandleCount { get; set; }
        public ulong WorkingSetSize { get; set; }
        public ulong VirtualSize { get; set; }
        //public string CreationDate { get; set; }
        public uint Priority { get; set; }
        public ulong UserModeTime { get; set; }
        public ulong KernelModeTime { get; set; }

        public ulong ReadOperationCount { get; set; }
        public ulong ReadTransferCount { get; set; }
        public ulong WriteOperationCount { get; set; }
        public ulong WriteTransferCount { get; set; }
        public uint SessionId { get; set; }
        public string OsName { get; set; }
        public string CreationClassName { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }

        public string OwnerInfo { get; set; }
        //public string UserName_ { get; set; }
        public string Domain { get; set; }

        // just a string constant
        public const string processName = "ProcessName";
    }
    public class ProcessInfoClass
    {

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool EnumProcessModules(IntPtr hProcess, [Out] IntPtr[] lphModuleBase, int cb, out int lpcbNeeded);

        [DllImport("kernel32.dll")]
        static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, int nSize);


        public string GetParentProcessName(ProcessEntry32.PROCESSENTRY32 processEntry32)
        {
            string parentProcessName = "";
            IntPtr hProcess = OpenProcess(0x1000, false, (int)processEntry32.th32ParentProcessID);
            if (hProcess != IntPtr.Zero)
            {
                try
                {
                    IntPtr[] moduleHandle = new IntPtr[1];
                    int cbNeeded;
                    if (EnumProcessModules(hProcess, moduleHandle, moduleHandle.Length * IntPtr.Size, out cbNeeded))
                    {
                        StringBuilder processName = new StringBuilder(1024);
                        GetModuleFileNameEx(hProcess, moduleHandle[0], processName, processName.Capacity);
                        parentProcessName = processName.ToString();
                    }
                }
                finally
                {
                    CloseHandle(hProcess);
                }
            }
            return parentProcessName;
        }
        // Method to create a ProcessInfo object
        public ProcessInfo CreateProcessInfo(ManagementObject mo)
        {
            ProcessInfo processInfo = new ProcessInfo();
            try
            {
                if (mo != null)
                {
                    //processInfo.ProcessName = processEntry32.szExeFile;
                    //processInfo.PID = processEntry32.th32ProcessID;
                    //processInfo.PPID = processEntry32.th32ParentProcessID;
                    //processInfo.ParentProcessName = GetParentProcessName(processEntry32);

                    //processInfo.ThreadCount = processEntry32.cntThreads;
                    //ProcessName = (string)mo["Name"],
                    //PID = (uint)mo["ProcessId"],
                    //PPID = (uint)mo["ParentProcessId"],
                    processInfo.ParentProcessName = GetParentProcessName((uint)mo["ParentProcessId"]);
                    processInfo.CommandLine = (string)mo["CommandLine"]?.ToString() ?? string.Empty;

                    processInfo.ExecutablePath = (string)mo["ExecutablePath"];
                    processInfo.ThreadCount = (uint)mo["ThreadCount"];
                    processInfo.HandleCount = (uint)mo["HandleCount"];
                    processInfo.WorkingSetSize = (ulong)mo["WorkingSetSize"];  //The amount of physical memory used by the process
                    processInfo.VirtualSize = (ulong)mo["VirtualSize"];
                    //CreationDate = ManagementDateTimeConverter.ToDateTime((string)mo["CreationDate"]),
                    processInfo.CreationDate = (ManagementDateTimeConverter.ToDateTime((string)mo["CreationDate"]).ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
                    processInfo.Priority = (uint)mo["Priority"];
                    processInfo.UserModeTime = (ulong)mo["UserModeTime"];
                    processInfo.KernelModeTime = (ulong)mo["KernelModeTime"];

                    processInfo.ReadOperationCount = (ulong)mo["ReadOperationCount"];
                    processInfo.ReadTransferCount = (ulong)mo["ReadTransferCount"];
                    processInfo.WriteOperationCount = (ulong)mo["WriteOperationCount"];
                    processInfo.WriteTransferCount = (ulong)mo["WriteTransferCount"];
                    processInfo.SessionId = (uint)mo["SessionId"];
                    processInfo.OsName = (string)mo["OSName"];
                    processInfo.CreationClassName = (string)mo["CreationClassName"];

                    processInfo.Description = (string)mo["Description"];

                    processInfo.OwnerInfo = mo["__PATH"].ToString();
                    processInfo.Domain = mo["__PATH"].ToString().Split('\\')[2];
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return processInfo;
            }

            return processInfo;
        }


        /// <summary>
        /// This is for settings the property width to every parameter
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class ThisParameterColumnWidthAttribute : Attribute
        {
            public int Width { get; }
            public ThisParameterColumnWidthAttribute(int width)
            {
                Width = width;
            }
        }
        static string GetParentProcessName(uint parentProcessId)
        {
            string parentProcessName = "";
            try
            {
                parentProcessName = Regex.Match(Process.GetProcessById((int)parentProcessId).ToString(), @"\((.*?)\)").Groups[1].Value;
            }
            catch (Exception e)
            {
                parentProcessName = e.Message.ToString();
            }
            return parentProcessName;
        }
    }
}
