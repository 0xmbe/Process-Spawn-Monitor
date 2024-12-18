using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;

//using static Process_Spawn_Monitor.DebugFileLogger;

/// <summary>
/// Takes care of handling with specific processes on low level.
/// </summary>
namespace ProcessManagerLib
{

    [Flags]
    public enum ProcessAccess : uint
    {
        Terminate = 0x00000001,
        CreateThread = 0x00000002,
        VMOperation = 0x00000008,
        VMRead = 0x00000010,
        VMWrite = 0x00000020,
        DupHandle = 0x00000040,
        SetInformation = 0x00000200,
        QueryInformation = 0x00000400,
        SuspendResume = 0x00000800,
        Synchronize = 0x00100000,
        All = 0x001F0FFF
    }

    public static class NativeMethods
    {
        [DllImport("ntdll.dll", EntryPoint = "NtSuspendProcess", SetLastError = true)]
        public static extern uint SuspendProcess(IntPtr processHandle);

        [DllImport("ntdll.dll", EntryPoint = "NtResumeProcess", SetLastError = true)]
        public static extern uint ResumeProcess(IntPtr processHandle);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(ProcessAccess dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

        //[DllImport("kernel32.dll", SetLastError = true)]
        //public static extern bool GetExitCodeProcess(IntPtr hProcess, ulong lpExitCode);
    }

    public class ProcessManager
    {
        private static ProcessManager _ProcessManager;
        private readonly Action<string> _updateDebugWindow;
        private ProcessManager(Action<string> updateDebugWindow)
        {
            _updateDebugWindow = updateDebugWindow;
        }
        public static void Initialize(Action<string> updateDebugWindow)
        {
            if (_ProcessManager == null)
            {
                _ProcessManager = new ProcessManager(updateDebugWindow);
            }
        }
        private static void __updateDebugWindow(string text)
        {
            if (_ProcessManager != null)
            {
                _ProcessManager._updateDebugWindow(text);
            }
            else
            {
                __updateDebugWindow("ProcessManager is not initialized.");
            }
        }
        private readonly Action<string, uint, uint, string> _updateDataGridView;
        public ProcessManager(Action<string, uint, uint, string> updateDataGridView)
        {
            _updateDataGridView = updateDataGridView;
        }

        public static bool SuspendProcess(int processId)
        {
            try
            {
                if (processId < 1)
                {
                    return false;
                }
                __updateDebugWindow($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")} {MethodBase.GetCurrentMethod().Name}: Starting on process {processId}.");

                IntPtr processHandle = NativeMethods.OpenProcess(ProcessAccess.SuspendResume, false, (uint)processId);
                if (processHandle == IntPtr.Zero)
                {
                    __updateDebugWindow($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")} {MethodBase.GetCurrentMethod().Name}: Unable to open process {processId}. Not elevated? Process doesn't exist anymore?");
                    return false;
                }

                uint result = NativeMethods.SuspendProcess(processHandle);
                NativeMethods.CloseHandle(processHandle);

                if (result != 0)
                {
                    _ProcessManager._updateDebugWindow($"{MethodBase.GetCurrentMethod().Name}: Failed, returned: {result}, HEX = {result.ToString("X")}");
                    return false;
                }

                // If no errors it should be suspended:
                _ProcessManager._updateDebugWindow($"{MethodBase.GetCurrentMethod().Name}: Process {processId} Suspended.");
            }
            catch (Exception ex)
            {
                __updateDebugWindow($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")} {MethodBase.GetCurrentMethod().Name}: Exception: {ex.Message}\nStack Trace: {ex.StackTrace}");

            }

            return true;
        }

        public static bool ResumeProcess(int processId)
        {
            if (processId < 1)
            {
                return false;
            }
            __updateDebugWindow($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")} {MethodBase.GetCurrentMethod().Name}: Starting on process {processId}.");
            IntPtr processHandle = NativeMethods.OpenProcess(ProcessAccess.SuspendResume, false, (uint)processId);
            if (processHandle == IntPtr.Zero)
            {
                __updateDebugWindow($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")} {MethodBase.GetCurrentMethod().Name}: Unable to open process {processId}. Process doesn't exist anymore?");
                return false;
            }

            uint result = NativeMethods.ResumeProcess(processHandle);
            NativeMethods.CloseHandle(processHandle);

            if (result != 0)
            {
                _ProcessManager._updateDebugWindow($"{MethodBase.GetCurrentMethod().Name}: Failed, returned: {result}, HEX = {result.ToString("X")}");
                return false;
            }
            __updateDebugWindow($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")} {MethodBase.GetCurrentMethod().Name}: Process {processId} resumed!");
            return true;
        }
        public static bool KillProcess(int processId)
        {
            try
            {
                if (processId < 1)
                {
                    return false;
                }
                __updateDebugWindow($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")} {MethodBase.GetCurrentMethod().Name}: Starting on process {processId}.");
                // Open the process with the desired access rights
                IntPtr processHandle = NativeMethods.OpenProcess(ProcessAccess.Terminate, false, (uint)processId);
                if (processHandle == IntPtr.Zero)
                {
                    __updateDebugWindow($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")} {MethodBase.GetCurrentMethod().Name}: Failed {processId}. Process doesn't exist anymore?");
                    return false;
                }
                if (NativeMethods.TerminateProcess(processHandle, (uint)ProcessAccess.Terminate))
                {
                    __updateDebugWindow($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")} {MethodBase.GetCurrentMethod().Name}: Process {processId} terminated successfully!");
                    return true;
                }
                __updateDebugWindow($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")} {MethodBase.GetCurrentMethod().Name}: Failed to terminate the process {processId}.");
                return false;

                // This is more higher level process termination:
                //Process process = Process.GetProcessById(processId);
                //process.Kill();
                //if (process.WaitForExit(5000))
                //{
                //    __updateDebugWindow($"Process {processId} has been killed.");
                //    return true;
                //}
                //else
                //{
                //    __updateDebugWindow($"Can't kill Process {processId}.");
                //    return false;
                //}
            }
            catch (Exception ex)
            {
                __updateDebugWindow($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")} {MethodBase.GetCurrentMethod().Name}: Failed to kill process {processId}. Error: {ex.Message}");
                return false;
            }
        }
        public static bool KeepSuspended(int processId)
        {
            __updateDebugWindow($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")} {MethodBase.GetCurrentMethod().Name}: Starting on process {processId}.");
            return true;
        }

        private IntPtr _processHandle;

       

        public void Dispose()
        {
            if (_processHandle != IntPtr.Zero)
            {
                NativeMethods.CloseHandle(_processHandle);
                _processHandle = IntPtr.Zero;
            }
        }


        //internal void Dispose()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
