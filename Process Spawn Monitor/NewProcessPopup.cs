using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProcessManagerLib;
using ProcessInfoLib;
using System.Runtime.InteropServices;

namespace Process_Spawn_Monitor
{
    /// <summary>
    /// This is pop-up form that warns user about newely created process.
    /// </summary>
    public partial class NewProcessPopup : Form
    {
        private ProcessInfo currentProcess;
        private int currentProcessId;
        private ProcessEntry32.PROCESSENTRY32 processEntry32;


        //private int processId;
        //private string processName;
        //private string parentProcessName;
        //private bool isSuspendedParentProcess;

        public NewProcessPopup()
        {
            InitializeComponent();
            //this.Show();
        }
        public NewProcessPopup(ProcessInfo currentProcess)          // What is the point of this method -> DELETE??
        {
            InitializeComponent();
            this.currentProcess = currentProcess;
            //ShowForm();
        }
        public NewProcessPopup(ProcessEntry32.PROCESSENTRY32 processEntry32)
        {
            //this.InitializeComponent();
            //this.Show();
            //NewProcessPopup newProcessPopup = new NewProcessPopup(processEntry32);
            //newProcessPopup.Show();
            //InitializeComponent();
            this.processEntry32 = processEntry32;
            //this.ShowForm();
            InitializeComponent();
            FillLabelsText();
        }


        private void button_allowProcess_Click(object sender, EventArgs e)
        {
            ProcessManager.ResumeProcess((int)currentProcess.PID);
            this.Close();
        }

        private void button_killProcess_Click(object sender, EventArgs e)
        {
            ProcessManager.KillProcess((int)currentProcess.PID);
            this.Close();
        }

        public void FillLabelsText()
        {

            Label labelRunningProcess = new Label
            {
                Text = $"Name: {processEntry32.szExeFile}",
                AutoSize = true,
                Font = new System.Drawing.Font("Lucida Console", 9),
                ForeColor = System.Drawing.Color.Black,
                Location = new System.Drawing.Point(20, 20)
            };

            Label labelProcessID = new Label
            {
                Text = $"ID: {processEntry32.th32ProcessID}",
                AutoSize = true,
                Font = new System.Drawing.Font("Lucida Console", 9),
                ForeColor = System.Drawing.Color.Black,
                Location = new System.Drawing.Point(20, 40)
            };

            Label labelParentProcessName = new Label
            {
                Text = $"Parent Process Name: {processEntry32.th32ParentProcessID}",
                AutoSize = true,
                Font = new System.Drawing.Font("Lucida Console", 9),
                ForeColor = System.Drawing.Color.Black,
                Location = new System.Drawing.Point(20, 60)
            };
            //Label labelParentProcessID = new Label
            //{
            //    Text = $"Parent Process ID: {currentProcess.PPID}",
            //    AutoSize = true,
            //    Font = new System.Drawing.Font("Lucida Console", 9),
            //    ForeColor = System.Drawing.Color.Black,
            //    Location = new System.Drawing.Point(20, 80)
            //};
            //Label labelCommandLine = new Label
            //{
            //    Text = $"Command line: {processEntry32.CommandLine}",
            //    AutoSize = true,
            //    Font = new System.Drawing.Font("Lucida Console", 9),
            //    ForeColor = System.Drawing.Color.Black,
            //    Location = new System.Drawing.Point(20, 100)
            //};

            this.Controls.Add(labelRunningProcess);
            this.Controls.Add(labelProcessID);
            this.Controls.Add(labelParentProcessName);
            //this.Controls.Add(labelParentProcessID);
            //this.Controls.Add(labelCommandLine);
        }

        private void button_keepSuspended_Click(object sender, EventArgs e)
        {
            ProcessManager.KeepSuspended((int)processEntry32.th32ProcessID);
            this.Close();
        }
        private void button_TerminateThisTimeOnly_Click(object sender, EventArgs e)
        {
            ProcessManager.KillProcess((int)processEntry32.th32ProcessID);
            this.Close();
        }
        private void button_TerminateAlways_Click(object sender, EventArgs e)
        {
            ProcessManager.KillProcess((int)processEntry32.th32ProcessID);
            IniFileManager.AddToKnownProcessList(processEntry32.szExeFile, IniFileManager.OnNewDetectedUnlistedProcess.Terminate.ToString());
            this.Close();
        }
        private void button_AllowThisTimeOnly_Click(object sender, EventArgs e)
        {
            ProcessManager.ResumeProcess((int)processEntry32.th32ProcessID);
            this.Close();
        }
        private void button_AllowAlways_Click(object sender, EventArgs e)
        {
            //ProcessManager.ResumeProcess((int)processEntry32.th32ProcessID);
            IniFileManager.AddToKnownProcessList(processEntry32.szExeFile, IniFileManager.OnNewDetectedUnlistedProcess.DoNothing.ToString());
            this.Close();
        }
        private void button_SuspendThisTimeOnly_Click(object sender, EventArgs e)
        {
            ProcessManager.SuspendProcess((int)processEntry32.th32ProcessID);
            this.Close();
        }
        private void button_SuspendAlways_Click(object sender, EventArgs e)
        {
            ProcessManager.SuspendProcess((int)processEntry32.th32ProcessID);
            IniFileManager.AddToKnownProcessList(processEntry32.szExeFile, IniFileManager.OnNewDetectedUnlistedProcess.SuspendAndAsk.ToString());
            this.Close();
        }
    }
}
