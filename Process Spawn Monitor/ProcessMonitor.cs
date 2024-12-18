using System;
using System.Diagnostics;
using System.Management;
using System.Text.RegularExpressions;
using System.Threading;

using ProcessManagerLib;
using ProcessInfoLib;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.Threading;
using ProcessInfoLib;
using System.Management;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections;
using System.Reflection;
using IniParser;

using System.Windows.Forms;
using System.Linq;
using static Process_Spawn_Monitor.ProcessEntry32;
using System.Runtime.Remoting.Messaging;
using System.Drawing;

namespace Process_Spawn_Monitor
{
    /// <summary>
    /// Takes care of monitoring processes, user forms
    /// </summary>
    public class ProcessMonitor
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr CreateToolhelp32Snapshot(uint dwFlags, uint th32ProcessID);

        //[DllImport("kernel32.dll")]
        //static extern bool Process32First(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        //[DllImport("kernel32.dll")]
        //static extern bool Process32Next(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

        //[StructLayout(LayoutKind.Sequential)]
        //struct PROCESSENTRY32
        //{
        //    public uint dwSize;
        //    public uint cntUsage;
        //    public uint th32ProcessID;
        //    public uint th32DefaultHeapID;
        //    public uint th32ModuleID;
        //    public uint cntThreads;
        //    public uint th32ParentProcessID;
        //    public int pcPriClassBase;
        //    public int dwFlags;
        //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        //    public string szExeFile;
        //}
        //private static readonly string[] IgnoredProcesses =
        //{
        //    "dllhost.exe", "SearchProtocolHost.exe", "SearchFilterHost.exe",
        //    "taskhost.exe", "conhost.exe", "backgroundTaskHost.exe",
        //    "explorer.exe", "chrome.exe", "Taskmgr.exe", "ScriptedSandbox64.exe",
        //    "svchost", "svchost.exe", "audiodg.exe", "msedge.exe"
        //};


        private FormMain formMain;

        private List<string> ToBeKilledProcesses = new List<string>();
        private List<string> ToBeKilledProcessesContainingCommandLines = new List<string>();


        private static readonly TimeSpan NewProcessCheckInterval = TimeSpan.FromMilliseconds(250);  //250
        private const bool SuspendParentProcess = false;
        private readonly Action<string> _updateDebugWindow;
        private readonly Action<ProcessInfo> _updateRichTextBox2;
        private readonly Action<ProcessInfo> _showPopupAction;
        //private readonly Action<string> _updateNotifications;
        public NotificationsForm _NotificationsForm;

        public ProcessMonitor(
            Action<string> updateRichTextBox,
            Action<ProcessInfo> showPopupAction,
            Action<ProcessInfo> updateDataGridView2
            //Action<string> updateNotifications
            )
        {
            _updateDebugWindow = updateRichTextBox;
            _showPopupAction = showPopupAction;
            _updateDataGridView2 = updateDataGridView2;
            //_updateNotifications = updateNotifications;


            // RUN ON PROCESSMONITOR INIT:
            NotificationsForm notificationsForm = new NotificationsForm();
            _NotificationsForm = notificationsForm;
            _NotificationsForm.Show();
            //RunNotificationsOnThread();
        }

        private readonly Action<string, uint, uint, string> _updateDataGridView;
        private readonly Action<ProcessInfo> _updateDataGridView2;

        private List<uint> previousProcessIds = new List<uint>();
        private object threadLock_monitoring = new object();

        //public ProcessMonitor(Action<string, uint, uint, string> updateDataGridView)
        //{
        //    _updateDataGridView = updateDataGridView ?? throw new ArgumentNullException(nameof(updateDataGridView));
        //}
        public ProcessMonitor(Action<ProcessInfo> updateDataGridView2)
        {
            _updateDataGridView2 = _updateDataGridView2 ?? throw new ArgumentNullException(nameof(_updateDataGridView2));
        }

        public void RunNotificationsOnThread()
        {
            var thread = new Thread(RunNotifications);
            thread.IsBackground = true;
            thread.Start();
        }
        private void RunNotifications()
        {
            _NotificationsForm.UpdateNotificationsWindow("test");
        }


        //    }
        //}
        /// <summary>
        /// A private method that uses local data (list). Use this to call from outside
        /// </summary>
        public void RunMonitoring()
        {
            // Load filters here:
            //LoadProcessMonitorFilters();
            ToBeKilledProcesses = FormMain.LoadIniFileFilters("ToBeKilledProcesses");
            ToBeKilledProcessesContainingCommandLines = FormMain.LoadIniFileFilters("ToBeKilledProcessesContainingCommandLines");

            //NotificationsForm notificationsForm = new NotificationsForm();
            //this._NotificationsForm = notificationsForm;
            //notificationsForm.ShowDialog();
            //_NotificationsForm.ShowDialog();
            //_NotificationsForm.Show();
            //RunNotificationsOnThread();


            // Finally start Monitoring:
            RunMonitoringIndefinelly(ref previousProcessIds, 50);                     //-------->
        }
        private void RunMonitoringIndefinelly(ref List<uint> previousProcessIds, int sleepTime)
        {
            //ProcessEntry32.PROCESSENTRY32 processEntry32 = new ProcessEntry32.PROCESSENTRY32();
            //processEntry.dwSize = (uint)Marshal.SizeOf(typeof(PROCESSENTRY32));
            //IntPtr hSnapshot = CreateToolhelp32Snapshot(0x00000002, 0);
            //List<uint> previousProcessIds = new List<uint>();


            //lock (threadLock_monitoring)      // Lock to prevent other threads of accessing referenced list
            {
                while (true)
                {
                    // Create a new snapshot
                    IntPtr hSnapshot = CreateToolhelp32Snapshot(0x00000002, 0);
                    if (hSnapshot == IntPtr.Zero)
                    {
                        _updateDebugWindow("Failed to create process snapshot.");
                        break;
                    }

                    ProcessEntry32.PROCESSENTRY32 processEntry32 = new ProcessEntry32.PROCESSENTRY32();
                    processEntry32.dwSize = (uint)Marshal.SizeOf(typeof(ProcessEntry32.PROCESSENTRY32));

                    if (!ProcessEntry32.Process32First(hSnapshot, ref processEntry32))
                    {
                        CloseHandle(hSnapshot);
                        _updateDebugWindow("Failed to get first process.");
                        break;
                    }

                    // Compare with previous snapshot
                    while (ProcessEntry32.Process32Next(hSnapshot, ref processEntry32))
                    {
                        if (!previousProcessIds.Contains(processEntry32.th32ProcessID))
                        {
                            try
                            {
                                // New process detected         // --> THIS METHOD IS A BIT SLOW SO WE CAN MISS THE PROCESS LIFE
                                // slow:
                                //ManagementObject managementObject = GetManagementObjectByProcessId(processEntry32);

                                ManagementObject managementObject = GenerateManagementObjectFromProcessEntry32(processEntry32);

                                ProcessInfoClass pic = new ProcessInfoClass();
                                ProcessInfo currentProcess = pic.CreateProcessInfo(managementObject);
                                currentProcess.PID = processEntry32.th32ProcessID;
                                currentProcess.ProcessName = processEntry32.szExeFile;
                                currentProcess.PPID = processEntry32.th32ParentProcessID;

                                _updateDataGridView2(currentProcess);
                                _updateDebugWindow($"New process started: {currentProcess.CreationDate} {processEntry32.szExeFile} ({processEntry32.th32ProcessID})");
                                previousProcessIds.Add(processEntry32.th32ProcessID);



                                if (Notifications.CheckNotificationsEnabled())
                                {
                                    if (_NotificationsForm.IsDisposed == false || _NotificationsForm != null)
                                    {
                                        _NotificationsForm.UpdateNotificationsWindow($"{processEntry32.szExeFile} ({processEntry32.th32ProcessID})");
                                    }
                                }


                                if (IniFileManager.CheckProcessKeyExists(IniFileManager.knownProcessList, processEntry32))
                                {
                                    ActAccordingToKnownProcessList(processEntry32);
                                }
                                else
                                {
                                    ActAccordingToGeneralPreferencesRules(processEntry32);
                                }


                                ProcessMonitorFilter_CommandLine(currentProcess);
                            }
                            catch (Exception e)
                            {
                                _updateDebugWindow($"{processEntry32.szExeFile}:\n{e.ToString()}");
                            }
                        }
                    }
                    CloseHandle(hSnapshot);

                    Thread.Sleep(sleepTime);
                }
            }
        }

        private void ActAccordingToKnownProcessList(ProcessEntry32.PROCESSENTRY32 processEntry32)
        {
            var parser = new FileIniDataParser();
            var data = IniFileManager.LoadIniFile(IniFileManager.iniFileName);

            //if (ToBeKilledProcessesContainingCommandLines.Any(processCommandLine => currentProcess.CommandLine.Contains(processCommandLine)))

            if (data[IniFileManager.knownProcessList][processEntry32.szExeFile] 
                == IniFileManager.OnNewDetectedUnlistedProcess.Terminate.ToString())
            {
                ProcessManager.KillProcess((int)processEntry32.th32ProcessID);
            }
            // Do nothing if not terminate.
            // This needs to be like that so TerminateOnlyKnownLeaveOthers works correctly.
        }
        private void ActAccordingToGeneralPreferencesRules(ProcessEntry32.PROCESSENTRY32 processEntry32)
        {
            if (IniFileManager.GetKeyValue("ProcessManager", "OnNewDetectedUnlistedProcess")
                == IniFileManager.OnNewDetectedUnlistedProcess.Terminate.ToString())
            {
                ProcessManager.KillProcess((int)processEntry32.th32ProcessID);
            }
            if (IniFileManager.GetKeyValue("ProcessManager", "OnNewDetectedUnlistedProcess")
                == IniFileManager.OnNewDetectedUnlistedProcess.TerminateOnlyKnownLeaveOthers.ToString())
            {
                ActAccordingToKnownProcessList(processEntry32);
                //ProcessManager.KillProcess((int)processEntry32.th32ProcessID);
            }
            if (IniFileManager.GetKeyValue("ProcessManager", "OnNewDetectedUnlistedProcess") 
                == IniFileManager.OnNewDetectedUnlistedProcess.SuspendAndAsk.ToString())
            {
                ProcessManager.SuspendProcess((int)processEntry32.th32ProcessID);
                NewProcessPopup newProcessPopup = new NewProcessPopup(processEntry32);
            }
            if (IniFileManager.GetKeyValue("ProcessManager", "OnNewDetectedUnlistedProcess") 
                == IniFileManager.OnNewDetectedUnlistedProcess.Ask.ToString())
            {
                FormMain._FormMain.Invoke((MethodInvoker)delegate ()
                {
                    NewProcessPopup newForm = new NewProcessPopup(processEntry32);
                    newForm.Show();
                });
            }
            if (IniFileManager.GetKeyValue("ProcessManager", "OnNewDetectedUnlistedProcess") 
                == IniFileManager.OnNewDetectedUnlistedProcess.DoNothing.ToString())
            {
                // nothing
            }
        }

        /// <summary>
        /// CAN YOU IMAGINE A WORLD WITHOUT A CRASHPAD ?
        /// </summary>
        /// <param name="currentProcess"></param>
        private void ProcessMonitorFilter_CommandLine(ProcessInfo currentProcess)
        {
            try
            {
                if (currentProcess.CommandLine != null)
                {
                    if (ToBeKilledProcessesContainingCommandLines.Any(processCommandLine => currentProcess.CommandLine.Contains(processCommandLine)))
                    {
                        _updateDebugWindow($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")} {MethodBase.GetCurrentMethod().Name} Detected: {currentProcess.ProcessName}");
                        ProcessManager.KillProcess((int)currentProcess.PID);
                        _updateDebugWindow($"Killed process data:\n" +
                            $"\tCommandLine: {currentProcess.CommandLine}\n" +
                            $"\tPID: {currentProcess.PID}\n" +
                            $"\tName: {currentProcess.ProcessName}\n" +
                            $"\tPPID: {currentProcess.PPID}\n" +
                            $"\tPP Name: {currentProcess.ParentProcessName}\n" +
                            $"\tThreads: {currentProcess.ThreadCount}\n");

                        DebugFileLogger debugFileLogger = new DebugFileLogger();
                        debugFileLogger.DebugLogToFile($"{MethodBase.GetCurrentMethod().Name}: Process {currentProcess.PID} terminated successfully!");
                        debugFileLogger.DebugLogToFile($"Killed process data:\n" +
                            $"\tCommandLine: {currentProcess.CommandLine}\n" +
                            $"\tPID: {currentProcess.PID}\n" +
                            $"\tName: {currentProcess.ProcessName}\n" +
                            $"\tPPID: {currentProcess.PPID}\n" +
                            $"\tPP Name: {currentProcess.ParentProcessName}\n" +
                            $"\tThreads: {currentProcess.ThreadCount}\n");
                    }
                }
            }
            catch (Exception e)
            {
                _updateDebugWindow(e.Message);
            }
        }
        private bool IsProcessOnTheKnownProcessList(ProcessEntry32.PROCESSENTRY32 processEntry32)
        {
            try
            {
                if (ToBeKilledProcesses.Contains(processEntry32.szExeFile))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                _updateDebugWindow(e.Message);
            }
            return false;
        }
        private void ProcessMonitorFilter_(ProcessEntry32.PROCESSENTRY32 processEntry32)
        {
            try
            {
                if (ToBeKilledProcesses.Contains(processEntry32.szExeFile))
                {
                    _updateDebugWindow($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")} Process to be killed detected: {processEntry32.szExeFile}");
                    ProcessManager.KillProcess((int)processEntry32.th32ProcessID);
                    _updateDebugWindow($"Killed process data:\n" +
                        $"\tPID: {processEntry32.th32ProcessID}\n" +
                        $"\tName: {processEntry32.szExeFile}\n" +
                        $"\tPPID: {processEntry32.th32ParentProcessID}\n" +
                        $"\tThreads: {processEntry32.cntThreads}\n");

                    DebugFileLogger debugFileLogger = new DebugFileLogger();
                    debugFileLogger.DebugLogToFile($"{MethodBase.GetCurrentMethod().Name}: Process {processEntry32.th32ProcessID} terminated successfully!");
                    debugFileLogger.DebugLogToFile($"Killed process data:\n" +
                        $"\tPID: {processEntry32.th32ProcessID}\n" +
                        $"\tName: {processEntry32.szExeFile}\n" +
                        $"\tPPID: {processEntry32.th32ParentProcessID}\n" +
                        $"\tThreads: {processEntry32.cntThreads}\n");
                }
            }
            catch (Exception e)
            {
                _updateDebugWindow(e.Message);
            }
        }
        private void ProcessMonitorFilter_ProcessName(ProcessEntry32.PROCESSENTRY32 processEntry32)
        {
            try
            {
                if (ToBeKilledProcesses.Contains(processEntry32.szExeFile))
                {
                    _updateDebugWindow($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")} Process to be killed detected: {processEntry32.szExeFile}");
                    ProcessManager.KillProcess((int)processEntry32.th32ProcessID);
                    _updateDebugWindow($"Killed process data:\n" +
                        $"\tPID: {processEntry32.th32ProcessID}\n" +
                        $"\tName: {processEntry32.szExeFile}\n" +
                        $"\tPPID: {processEntry32.th32ParentProcessID}\n" +
                        $"\tThreads: {processEntry32.cntThreads}\n");

                    DebugFileLogger debugFileLogger = new DebugFileLogger();
                    debugFileLogger.DebugLogToFile($"{MethodBase.GetCurrentMethod().Name}: Process {processEntry32.th32ProcessID} terminated successfully!");
                    debugFileLogger.DebugLogToFile($"Killed process data:\n" +
                        $"\tPID: {processEntry32.th32ProcessID}\n" +
                        $"\tName: {processEntry32.szExeFile}\n" +
                        $"\tPPID: {processEntry32.th32ParentProcessID}\n" +
                        $"\tThreads: {processEntry32.cntThreads}\n");
                }
            }
            catch (Exception e)
            {
                _updateDebugWindow(e.Message);
            }
        }

        private void LoadProcessMonitorFilters()
        {
            //string[] ToBeKilledProcesses = new string[2];

            //FormMain fm = new FormMain();
            //ToBeKilledProcesses = fm.GetProcessMonitorFilters();
            ToBeKilledProcesses = FormMain.LoadIniFileFilters("ToBeKilledProcesses");


        }
        //public void StartMonitoring()
        //{
        //    //var scope = new ManagementScope("\\\\.\\root\\cimv2");
        //    //var query = new WqlEventQuery("__InstanceCreationEvent", NewProcessCheckInterval, "TargetInstance ISA 'Win32_Process'");
        //    //var watcher = new ManagementEventWatcher(scope, query);

        //    // NEW:
        //    IntPtr hSnapshot = CreateToolhelp32Snapshot(0x00000002, 0);
        //    if (hSnapshot == IntPtr.Zero)
        //    {
        //        _updateDebugWindow("Failed to create process snapshot.");
        //        return;
        //    }

        //    ProcessEntry32.PROCESSENTRY32 processEntry = new ProcessEntry32.PROCESSENTRY32();
        //    processEntry.dwSize = (uint)Marshal.SizeOf(typeof(ProcessEntry32.PROCESSENTRY32));

        //    // TESTING;
        //    var a1 = (uint)Marshal.GetHINSTANCE(typeof(ProcessEntry32.PROCESSENTRY32).Module);
        //    //Type type = typeof(PROCESSENTRY32);
        //    //var a2 = (uint)Marshal.GetHINSTANCE(type.GetEvents().Length);

        //    if (!ProcessEntry32.Process32First(hSnapshot, ref processEntry))
        //    {
        //        CloseHandle(hSnapshot);
        //        _updateDebugWindow("Failed to get first process.");
        //        return;
        //    }
        //    // List of processes in this state:
        //    List<uint> previousProcessIds = new List<uint>();

        //    //---



        //    //if (watcher != null)
        //    {
        //        _updateDebugWindow("Monitoring newly spawned processes via WMI...");
        //        int processSpawnCounter = 1;

        //        if (_updateDataGridView2 == null)
        //        {
        //            throw new InvalidOperationException("UpdateDataGridView delegate is not assigned.");
        //        }

        //        while (true)
        //        {

        //            //var newlyArrivedEvent = watcher.WaitForNextEvent();
        //            //var mbo = (ManagementBaseObject)newlyArrivedEvent["TargetInstance"];
        //            _updateDebugWindow($"({processSpawnCounter}) New process spawned:");


        //            // NEW:
        //            // Iterate through the process list again
        //            if (!ProcessEntry32.Process32Next(hSnapshot, ref processEntry))
        //            {
        //                // If you are here that means there are no more processes to itterate over the snapshot
        //                break;
        //            }
        //            // Check if new process is started, save it on the list so it will not be visible next itteration:
        //            if (!previousProcessIds.Contains(processEntry.th32ProcessID))
        //            {
        //                _updateDebugWindow($"New process started:   {processEntry.szExeFile} {processEntry.th32ProcessID}");
        //                previousProcessIds.Add(processEntry.th32ProcessID);
        //            }
        //            else
        //            {
        //                // no process started, return
        //            }
        //            //---

        //            try
        //            {
        //                // Construct the correct path
        //                //string handle = mbo["Handle"].ToString();

        //                // Construct Management Object:


        //                //////////--------- PROCESS IS REALLY CREATED:. => SHOW DATA ON DATAGRIDVIEW:

        //                ManagementObject managementObject = GenerateManagementObjectFromProcessEntry32(processEntry);

        //                ProcessInfoClass pic = new ProcessInfoClass();
        //                ProcessInfo currentProcess = pic.CreateProcessInfo(managementObject);

        //                _updateDataGridView2(currentProcess);

        //                if (managementObject != null)
        //                {
        //                    //ProcessData(managementObject);
        //                }
        //                else
        //                {
        //                    _updateDebugWindow($"Failed to create ManagementObject from ManagementBaseObject.\n" +
        //                        //$"{newlyArrivedEvent}\n" +
        //                        //$"{mbo}\n" +
        //                        $"{currentProcess.PID}\n" +
        //                        // $"{path}\n" +
        //                        $"{managementObject}");
        //                }
        //                //}
        //                //catch (Exception ex)
        //                //{
        //                //    _updateDebugWindow($"Exception occurred: {ex.Message}" +
        //                //       $"{newlyArrivedEvent}\n" +
        //                //       $"{mbo}");
        //                //}


        //                //processSpawnCounter++;


        //                //string text = "host";// this.headers["host"] as string;
        //                //Regex regex = new Regex("^(\\w+\\.)*tekla\\.com(:\\d{1,5})?$", RegexOptions.IgnoreCase);
        //                //string r1 = regex.ToString();
        //                //Regex regex2 = new Regex("^127\\.0\\.0\\.1(:\\d{1,5})?$", RegexOptions.IgnoreCase);
        //                //Regex regex3 = new Regex("^localhost(:\\d{1,5})?$", RegexOptions.IgnoreCase);
        //                //if (!regex.IsMatch(text) && !regex2.IsMatch(text) && !regex3.IsMatch(text))
        //                //{ 
        //                //}


        //                _updateDebugWindow($"PID:\t\t{currentProcess.PID}");
        //                _updateDebugWindow($"Name:\t\t{currentProcess.ProcessName}");
        //                _updateDebugWindow($"PPID:\t\t{currentProcess.PPID}");


        //                _updateDebugWindow($"Parent name:\t{currentProcess.ParentProcessName}");
        //                _updateDebugWindow($"CommandLine:\t{currentProcess.CommandLine}");

        //                if (Array.IndexOf(IgnoredProcesses, currentProcess.ProcessName) == -1)
        //                {
        //                    if (ProcessManager.SuspendProcess((int)currentProcess.PID))
        //                    {
        //                        _updateDebugWindow("Process is suspended. Creating GUI popup.");
        //                        //_updateDataGridView(processName, processId, parentProcessId, commandLine);
        //                        //_updateDataGridView2(mbo);

        //                        if (SuspendParentProcess && currentProcess.ParentProcessName != "unknown")
        //                        {
        //                            currentProcess.ParentProcessName += ".exe";
        //                            if (Array.IndexOf(IgnoredProcesses, currentProcess.ParentProcessName) == -1)
        //                            {
        //                                _updateDebugWindow($">>Suspending parent of {currentProcess.ProcessName} : {currentProcess.ParentProcessName}");
        //                                if (ProcessManager.SuspendProcess((int)currentProcess.PPID))
        //                                {
        //                                    _updateDebugWindow(">>Suspended parent process. Creating GUI popup.");
        //                                    //_showPopupAction(currentProcess.ParentProcessName, (int)parentProcessId, processName, true);
        //                                    _showPopupAction(currentProcess);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                _updateDebugWindow($"Would have suspended parent process: {currentProcess.ParentProcessName}. But process is present in ignore list.");
        //                            }
        //                        }
        //                        //_showPopupAction(processName, (int)processId, currentProcess.ParentProcessName, false);

        //                        _showPopupAction(currentProcess);

        //                    }
        //                }
        //                else
        //                {
        //                    _updateDebugWindow("Process ignored as per configuration.");
        //                }

        //                _updateDebugWindow("");
        //                processSpawnCounter++;

        //                //watcher.Dispose();
        //            }
        //            catch (Exception ex)
        //            {
        //                _updateDebugWindow($"Exception occurred: {ex.Message}");
        //                //+$"{newlyArrivedEvent}\n" +
        //                //$"{mbo}");
        //            }

        //        }

        //        // NEW:
        //        CloseHandle(hSnapshot);



        //    }
        //    //if (watcher == null)
        //    //{
        //    //    _updateDebugWindow($"{watcher} is NULL");
        //    //}
        //}

        private static ManagementObject GenerateManagementObjectFromProcessEntry32(ProcessEntry32.PROCESSENTRY32 processEntry32)
        {
            string machineName = Environment.MachineName;
            string handle = processEntry32.th32ProcessID.ToString();
            ManagementScope scope = new ManagementScope("\\\\.\\root\\cimv2");
            string path = $"\\\\{machineName}\\root\\cimv2:Win32_Process.Handle=\"{handle}\"";
            ManagementObject managementObject = new ManagementObject(scope, new ManagementPath(path), null);
            return managementObject;
        }
        private ManagementObject GetManagementObjectByProcessId(ProcessEntry32.PROCESSENTRY32 processEntry32)
        {
            ManagementScope scope = new ManagementScope("\\\\.\\root\\cimv2");
            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Process WHERE ProcessId = " + processEntry32.th32ProcessID);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

            ManagementObjectCollection objectCollection = searcher.Get();
            if (objectCollection.Count > 0)
            {
                foreach (ManagementObject mo in objectCollection)
                {
                    ProcessInfoClass pic = new ProcessInfoClass();
                    ProcessInfo currentProcess = pic.CreateProcessInfo(mo);

                    _updateDataGridView2(currentProcess);
                    _updateDebugWindow($"New process started: {currentProcess.CreationDate} {processEntry32.szExeFile} ({processEntry32.th32ProcessID})");
                    previousProcessIds.Add(processEntry32.th32ProcessID);
                }
            }

            return null;
        }

        private static string[] GetProcessOwner(ManagementBaseObject mbo)
        {
            // Cast ManagementBaseObject to ManagementObject
            //ManagementObject mo = new ManagementObject(mbo["__PATH"].ToString());
            string[] ownerInfo = new string[2];
            using (ManagementObject mo = new ManagementObject(mbo["__PATH"].ToString()))
            {
                mo.InvokeMethod("GetOwner", ownerInfo);
            }
            return ownerInfo;
        }
    }
}
