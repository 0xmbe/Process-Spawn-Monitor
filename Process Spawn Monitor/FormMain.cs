using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Process_Spawn_Monitor;
using System.Threading;
using System.Management;

using System.Security.Cryptography;
using System.Net.Sockets;
using System.Net;

using static Process_Spawn_Monitor.ProcessMonitor;
using ProcessInfoLib;
using ProcessManagerLib;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;
using static ProcessInfoLib.ProcessInfoClass;

using IniParser;
using IniParser.Model;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using static Process_Spawn_Monitor.ProcessEntry32;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace Process_Spawn_Monitor
{
    public partial class FormMain : Form
    {
        public static FormMain _FormMain { get; set; }
        private ProcessMonitor _ProcessMonitor;
        private ProcessManager _ProcessManager;
        public DataGridView dataGridView;


        private int dataGridView_Spawns_verticalScrollPosition = 0;
        private int dataGridView_Spawns_horizontalScrollPosition = 0;

        public const string pluginsFolderName = "Plugins";



        public static async Task DelayAndExecuteAsync(Action action, TimeSpan delay)
        {
            await Task.Delay(delay);
            action();
        }
        /// <summary>
        /// Initializes Main form on startup. Runs only once.
        /// </summary>
        public FormMain()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            InitializeComponent();
            _FormMain = this;

            // After InitializeComponent:
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.Text = Assembly.GetExecutingAssembly().GetName().Name;
            this.Size = new Size(1435, 754);

            //LoadRequiredDllFiles();

            Initialize_DataGridView();



            _ProcessMonitor = new ProcessMonitor(
                UpdateDebugWindow,
                ShowNewProcessPopup,
                UpdateDataGridView2
                );

            // StartMonitoring();


            ProcessManager.Initialize(UpdateDebugWindow);


            //_ProcessMonitor.ListAllProcessesToDataGridView();
            //RunAThreadThatListsAllActiveProcessesAndWritesThemToOutput();
            RunAThreadThatMonitorsNewInstancesOfProcesses();
            // StartNewThreadFetchingDataOfAllActiveProcesses();


            LoadMainIniFileAndPopulateContexStriptMenu();

            //NotificationsForm notificationsForm = new NotificationsForm();
            // Notifications.Initialize(UpdateDebugWindow);         
            //Singleton.Instance.ShowNotification("Some message");


            //CreateMethod();

            Application.ApplicationExit += OnApplicationExit;
        }

        public void CreateMethod()
        {
            var csc = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });
            var parameters = new CompilerParameters(new[] { "mscorlib.dll", "System.Core.dll" });
            parameters.GenerateExecutable = false;
            CompilerResults results = csc.CompileAssemblyFromSource(parameters,

            @"
            using System.Linq;
            class MyClass {
              public void MyFunction() {
                var q = from i in Enumerable.Range(1,100)
                          where i % 2 == 0
                          select i;
              }
            }
            ");
            results.Errors.Cast<CompilerError>().ToList().ForEach(error => Console.WriteLine(error.ErrorText));
        }
        //class TestClass
        //{
        //    public void RunThisMethod()
        //    {
        //        System.Windows.Forms.MessageBox.Show("This Method instance from the string");
        //    }
        //}
        private void UpdateNotifications(string message)
        {
            if (debugWindow.InvokeRequired)
            {
                debugWindow.Invoke(new Action(() => debugWindow.AppendText(message + "\n")));
            }
            else
            {
                debugWindow.AppendText(message + "\n");
            }
        }

        /// <summary>
        /// DataGridView Properties settings, how grid looks
        /// </summary>
        private void Initialize_DataGridView()
        {
            dataGridView_Spawns.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8);
            dataGridView_Spawns.RowTemplate.Height = 18;

            // Get all column properties of ProcessInfo object:
            var properties = typeof(ProcessInfo).GetProperties();
            foreach (var property in properties)
            {
                using (DataGridViewColumn newColumn = new DataGridViewTextBoxColumn())
                {
                    newColumn.Name = property.Name;
                    newColumn.HeaderText = property.Name;
                    if (GetColumnWidth<ProcessInfo>(property.Name) > 0)
                    {
                        newColumn.Width = dataGridView_Spawns.Width = GetColumnWidth<ProcessInfo>(property.Name);
                    }
                    dataGridView_Spawns.Columns.Add(newColumn);
                }
            }
        }
        /// <summary>
        /// Gets the property of the Process Info Column Widht
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public int GetColumnWidth<T>(string propertyName)
        {
            var property = typeof(T).GetProperty(propertyName);
            var attribute = (ThisParameterColumnWidthAttribute)Attribute.GetCustomAttribute(property, typeof(ThisParameterColumnWidthAttribute));
            return attribute?.Width ?? 0; // Return 0 if the attribute is not found
        }
        private void RunAThreadThatMonitorsNewInstancesOfProcesses()
        {
            var fetchingThread = new Thread(RunMonitoring);
            fetchingThread.IsBackground = true;
            fetchingThread.Start();
        }
        private void RunMonitoring()
        {
            _ProcessMonitor.RunMonitoring();
        }
        /// <summary>
        /// Writes parameters to DataGridView rows about a specific process ProcessInfo.
        /// </summary>
        /// <param name="currentProcessInfo"></param>
        public void UpdateDataGridView2(ProcessInfo currentProcessInfo)
        {
            if (dataGridView_Spawns.IsHandleCreated)
            {
                if (dataGridView_Spawns.InvokeRequired)
                {
                    dataGridView_Spawns.Invoke(new Action(async () =>
                    {
                        var properties = typeof(ProcessInfo).GetProperties();
                        var values = properties.Select(p => p.GetValue(currentProcessInfo)).ToArray();
                        dataGridView_Spawns.Rows.Add(values);
                        dataGridView_Spawns.EndEdit(); // Commit change

                        // Also update the filter so we don't see new entries:
                        await DelayAndExecuteAsync(FilterDataGridViewWithSEARCH, TimeSpan.FromMilliseconds(100));
                        //FilterDataGridViewWithSEARCH();
                    }));
                }
                else
                {
                    var properties = typeof(ProcessInfo).GetProperties();
                    var values = properties.Select(p => p.GetValue(currentProcessInfo)).ToArray();
                    dataGridView_Spawns.Rows.Add(values);
                    dataGridView_Spawns.EndEdit(); // Move EndEdit here
                }
            }
        }
        /// <summary>
        /// Updates debug window
        /// </summary>
        /// <param name="message"></param>
        private void UpdateDebugWindow(string message)
        {
            if (debugWindow.InvokeRequired)
            {
                debugWindow.Invoke(new Action(() => debugWindow.AppendText(message + "\n")));
            }
            else
            {
                debugWindow.AppendText(message + "\n");
            }
        }
        private void UpdateDebugWindowsWithStringArray(string[] output)
        {
            foreach (string line in output)
            {
                debugWindow.AppendText(line + "\n");
            }
        }
        /// <summary>
        /// Shows user prompt when new process is detected.
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="processId"></param>
        /// <param name="parentProcessName"></param>
        /// <param name="isSuspendedParentProcess"></param>
        public void ShowNewProcessPopup(ProcessInfo currentProcess)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => ShowNewProcessPopup(currentProcess)));
            }
            else
            {
                NewProcessPopup popup = new NewProcessPopup(currentProcess);
                popup.ShowDialog();
            }
        }
        /// <summary>
        /// Auttomatic resizing of objects on Main form is handled here.
        /// </summary>
        public void ResizeFormObjects()
        {
            // Save and Restore DataGridView Scroll position:
            if (WindowState == FormWindowState.Minimized)
            {
                dataGridView_Spawns_verticalScrollPosition = dataGridView_Spawns.FirstDisplayedScrollingRowIndex;
                dataGridView_Spawns_horizontalScrollPosition = dataGridView_Spawns.FirstDisplayedScrollingColumnIndex;
            }
            //else  if (this.WindowState == FormWindowState.Normal || this.WindowState == FormWindowState.Maximized)
            else
            {
                // Restore the saved scroll position 
                if (dataGridView_Spawns_verticalScrollPosition >= 0 &&
                    dataGridView_Spawns_verticalScrollPosition < dataGridView_Spawns.Rows.Count &&
                    dataGridView_Spawns.Rows[dataGridView_Spawns_verticalScrollPosition].Visible == true)     //restore only to visible rows
                {
                    dataGridView_Spawns.FirstDisplayedScrollingRowIndex = dataGridView_Spawns_verticalScrollPosition;
                }
                if (dataGridView_Spawns_horizontalScrollPosition >= 0 &&
                    dataGridView_Spawns_horizontalScrollPosition < dataGridView_Spawns.Columns.Count)
                {
                    dataGridView_Spawns.FirstDisplayedScrollingColumnIndex = dataGridView_Spawns_horizontalScrollPosition;
                }
            }
        }
        /// <summary>
        /// Search box input string filtering rows in datagridview that contain that string
        /// </summary>
        private void FilterDataGridViewWithSEARCH()
        {
            // Filter the DataGridView, excluding the last (empty) row
            string searchText = textBox_SEARCH.Text.ToLower();
            for (int i = 0; i < dataGridView_Spawns.Rows.Count; i++)
            {
                DataGridViewRow row = dataGridView_Spawns.Rows[i];
                bool found = false;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString().ToLower().Contains(searchText))
                    {
                        found = true;
                        break;
                    }
                }
                row.Visible = found;
            }
        }
        /// <summary>
        /// Runs the external exe that accepts command line arguments and returns array of lines.
        /// EXE should be placed in same folder as Main exe.
        /// </summary>
        /// <param name="pathToExe"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private async Task<string[]> RunExternalProcessAsync(string ExeFilename, string arguments, bool outputToConsole)
        {
            string[] OutputLines = new string[0];
            try
            {
                string applicationPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                string fullPath = Path.Combine(Path.GetDirectoryName(applicationPath), ExeFilename);

                Process process = new Process();
                process.StartInfo.FileName = fullPath;
                process.StartInfo.Arguments = arguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = outputToConsole;

                process.Start();

                // Only if we want to debug to debugView:
                if (outputToConsole == true)
                {
                    string outputString = await Task.Run(() => process.StandardOutput.ReadToEnd());
                    OutputLines = outputString.Split(new[] { "\n" }, StringSplitOptions.None);
                }

                await Task.Run(() => process.WaitForExit());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return OutputLines;
        }

        private async void textBox_SEARCH_TextChanged(object sender, EventArgs e)
        {
            await DelayAndExecuteAsync(FilterDataGridViewWithSEARCH, TimeSpan.FromMilliseconds(100));
            //FilterDataGridViewWithSEARCH();
        }


        // RESIZING MAIN FORM:
        private void FormMain_Resize(object sender, EventArgs e)
        {
            ResizeFormObjects();
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            ResizeFormObjects();
        }
        private void OnApplicationExit(object sender, EventArgs e)
        {
            _ProcessManager?.Dispose();
        }

        /// <summary>
        /// This makes sure that when you click RMB the actual cell gets selected so you can get the data from selected cell when you run methods on context menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_Spawns_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dataGridView_Spawns.CurrentCell = dataGridView_Spawns.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
        }
        private int GetIntValueFromTheSelectedRowInDefinedColumn(string definedColumnName)
        {
            int cellValueInt;
            try
            {
                DataGridViewRow selectedRow = dataGridView_Spawns.Rows[dataGridView_Spawns.CurrentCell.RowIndex];
                DataGridViewCell selectedCell = selectedRow.Cells[definedColumnName];

                if (selectedCell.Value is uint value && value == 0)      // catching user clicking on edges of data grid view
                {
                    return 0;
                }
                if (selectedCell.Value != null)
                {
                    if (int.TryParse(selectedCell.Value.ToString(), out cellValueInt))
                    {
                        return cellValueInt;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return -1;
        }
        private string GetStringValueFromTheSelectedRowInDefinedColumn(string definedColumnName)
        {
            try
            {
                DataGridViewRow selectedRow = dataGridView_Spawns.Rows[dataGridView_Spawns.CurrentCell.RowIndex];
                DataGridViewCell selectedCell = selectedRow.Cells[definedColumnName];

                if (selectedCell.Value is string value && value == "")      // catching user clicking on edges of data grid view
                {
                    return "";
                }
                if (selectedCell.Value != null)
                {
                    return selectedCell.Value.ToString();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return "";
        }
        private string GetStringValueFromTheSelectedRowInSelectedColumn()
        {
            try
            {
                DataGridViewRow selectedRow = dataGridView_Spawns.Rows[dataGridView_Spawns.CurrentCell.RowIndex];
                DataGridViewCell selectedCell = selectedRow.Cells[dataGridView_Spawns.CurrentCell.ColumnIndex];

                if (selectedCell.Value is string value && value == "")      // catching user clicking on edges of data grid view
                {
                    return "";
                }
                if (selectedCell.Value != null)
                {
                    return selectedCell.Value.ToString();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return "";
        }
        private void LoadMainIniFileAndPopulateContexStriptMenu()
        {
            try
            {
                IniData data = IniFileManager.LoadMainIniFile();
                PopulateContextStripMenu(data);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        public static List<string> LoadIniFileFilters(string filtersName)
        {
            var data = IniFileManager.LoadIniFile(IniFileManager.iniFileName);
            List<string> filters = new List<string>();
            foreach (var line in data.Sections.GetSectionData(filtersName).Keys)
            {
                filters.Add(line.Value);
            }
            return filters;
        }
        private void PopulateContextStripMenu(IniData data)
        {
            List<ToolStripMenuItem> shellCommandMenuItems = new List<ToolStripMenuItem>();

            foreach (var line in data.Sections.GetSectionData("ShellCommands").Keys)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem(line.Value);
                menuItem.Click += async (sender, e) =>
                {
                    RunTerminalAsync("cmd.exe", line.Value);
                };
                shellCommandMenuItems.Add(menuItem);
            }
            usefullShellCommandsToolStripMenuItem.DropDownItems.Clear();
            usefullShellCommandsToolStripMenuItem.DropDownItems.AddRange(shellCommandMenuItems.ToArray());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TerminalType">cmd.exe or PowerShell.exe</param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public async Task RunTerminalAsync(string TerminalType, string arguments)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.Verb = "runas"; // Run as administrator
                startInfo.WorkingDirectory = @"%SystemRoot%\System32";

                if (TerminalType == "cmd.exe")
                {
                    startInfo.FileName = @"cmd.exe";
                    startInfo.Arguments = "/K " + arguments; // Use /K to keep the terminal open
                }
                else if (TerminalType == "PowerShell.exe")
                {
                    startInfo.FileName = @"powershell.exe";
                    startInfo.Arguments = "-NoExit -Command \"" + arguments + "\""; // Use -NoExit to keep the terminal open
                }

                using (Process process = Process.Start(startInfo))
                {
                    await Task.Run(() => process.WaitForExit());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error: {e.Message}");
            }
        }
        private void openPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var filePath = GetStringValueFromTheSelectedRowInDefinedColumn("ExecutablePath");
            //string argument_BAD = $"/select, \"{filePath}\"";                           // -> Looks like $ bug ??, output string is same
            string argument = "/select, \"" + filePath + "\"";
            Process.Start("explorer.exe", argument);
        }
        private void copySelectedValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var value = GetStringValueFromTheSelectedRowInSelectedColumn();
            Clipboard.SetText(value);
        }
        private void killProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessManager.KillProcess(GetIntValueFromTheSelectedRowInDefinedColumn("PID"));
        }
        private void suspendProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessManager.SuspendProcess(GetIntValueFromTheSelectedRowInDefinedColumn("PID"));
        }
        private void resumeProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessManager.ResumeProcess(GetIntValueFromTheSelectedRowInDefinedColumn("PID"));
        }

        private void dataGridView_Spawns_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Select whole line?
        }

        private async void tcpvcon64exeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] output = await RunExternalProcessAsync($"{pluginsFolderName}\\tcpvcon64.exe", "", true);
            UpdateDebugWindowsWithStringArray(output);
        }

        private async void netdumpexeToolStripMenuItem_ClickAsync(object sender, EventArgs e)
        {
            string argument = textBox_Arguments.Text.ToString();
            string[] output = await RunExternalProcessAsync($"{pluginsFolderName}\\netdump.exe", argument, false);
        }

        private async void netfilterexeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string argument = textBox_Arguments.Text.ToString();
            string[] output = await RunExternalProcessAsync($"{pluginsFolderName}\\netfilter.exe", argument, false);
        }

        private void pEMultiFileScannerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string selectedPath = dialog.SelectedPath;
                Task.Run(() => ScanFilesInDirectoryAsync(selectedPath, "*.exe"));
            }
        }

        public void ScanFilesInDirectoryAsync(string selectedPath, string fileExtension)
        {
            try
            {
                var searchOption = SearchOption.AllDirectories;
                var exes = Directory.EnumerateFiles(selectedPath, fileExtension, searchOption);

                foreach (var exe in exes)
                {
                    CheckFileSignature(exe);
                }
            }
            catch (Exception e)
            {
                UpdateDebugWindow(e.ToString());
            }
        }
        public void CheckFileSignature(string filePath)
        {
            try
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);
                // byte[] sample = { FF, 25, 00, 20 };

                string sampleString = "FF250020";           // -> Signature for C# software (Based on PEId.exe userdb.txt) 
                byte[] sampleBytes = new byte[sampleString.Length / 2];

                for (int i = 0; i < sampleString.Length; i += 2)
                {
                    sampleBytes[i / 2] = Convert.ToByte(sampleString.Substring(i, 2), 16);
                }

                //LoadIniFile("userdb.txt.ini");


                if (ContainsArray(fileBytes, sampleBytes))
                {
                    UpdateDebugWindow(filePath);
                }

            }
            catch (Exception ex)
            {
                UpdateDebugWindow("Error checking signature: " + ex.Message);
            }
        }
        public static bool ContainsArray(byte[] haystack, byte[] needle)
        {
            if (needle.Length > haystack.Length)
            {
                return false;
            }

            for (int i = 0; i <= haystack.Length - needle.Length; i++)
            {
                bool found = true;
                for (int j = 0; j < needle.Length; j++)
                {
                    if (haystack[i + j] != needle[j])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    return true;
                }
            }
            return false;
        }

        private void filtersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Preferences preferences = new Preferences();
            preferences.Show();
        }

        //private void addToAllowedProcessesToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    var parser = new FileIniDataParser();
        //    var data = FormMain.LoadIniFile(FormMain.iniFileName);
        //    var processName = GetStringValueFromTheSelectedRowInDefinedColumn("ProcessName");
        //    if (data[IniFileManager.knownProcessList][processName] != null)
        //    {
        //        data[IniFileManager.knownProcessList][processName] = "DoNothing";
        //    }
        //    else
        //    {
        //        data[IniFileManager.knownProcessList].AddKey(processName, "DoNothing");
        //    }
        //    parser.WriteFile(FormMain.iniFileName, data);
        //}

        private void alwaysAllowThisProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //IniFileManager.AddToKnownProcessList(GetStringValueFromTheSelectedRowInDefinedColumn("ProcessName"), IniFileManager.OnNewDetectedUnlistedProcess.DoNothing.ToString());
            var parser = new FileIniDataParser();
            var data = IniFileManager.LoadIniFile(IniFileManager.iniFileName);
            var processName = GetStringValueFromTheSelectedRowInDefinedColumn(ProcessInfo.processName);
            if (data[IniFileManager.knownProcessList][processName] != null)
            {
                data[IniFileManager.knownProcessList][processName] = IniFileManager.OnNewDetectedUnlistedProcess.DoNothing.ToString();
            }
            else
            {
                data[IniFileManager.knownProcessList].AddKey(processName, IniFileManager.OnNewDetectedUnlistedProcess.DoNothing.ToString());
            }
            parser.WriteFile(IniFileManager.iniFileName, data);
        }
        private void alwaysTerminateThisProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var parser = new FileIniDataParser();
            var data = IniFileManager.LoadIniFile(IniFileManager.iniFileName);
            var processName = GetStringValueFromTheSelectedRowInDefinedColumn(ProcessInfo.processName);
            if (data[IniFileManager.knownProcessList][processName] != null)
            {
                data[IniFileManager.knownProcessList][processName] = IniFileManager.OnNewDetectedUnlistedProcess.Terminate.ToString();
            }
            else
            {
                data[IniFileManager.knownProcessList].AddKey(processName, IniFileManager.OnNewDetectedUnlistedProcess.Terminate.ToString());
            }
            parser.WriteFile(IniFileManager.iniFileName, data);
        }
        private void removeFromKnownProcessListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var parser = new FileIniDataParser();
            var data = IniFileManager.LoadIniFile(IniFileManager.iniFileName);
            var processName = GetStringValueFromTheSelectedRowInDefinedColumn(ProcessInfo.processName);
            if (data[IniFileManager.knownProcessList][processName] != null)
            {
                data[IniFileManager.knownProcessList].RemoveKey(processName);
                parser.WriteFile(IniFileManager.iniFileName, data);
            }
        }
        private void openLogFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string logFilePath = DebugFileLogger.GetLogFilePath();
            if (File.Exists(logFilePath))
            {
                Process.Start(logFilePath);
            }
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadMainIniFileAndPopulateContexStriptMenu();
        }
    }

}