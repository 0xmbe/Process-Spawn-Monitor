
namespace Process_Spawn_Monitor
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.debugWindow = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip_RightMouseClickOnDataGridView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySelectedValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_KillProcess = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_SuspendProcess = new System.Windows.Forms.ToolStripMenuItem();
            this.resumeProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.alwaysAllowThisProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysTerminateThisProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFromKnownProcessListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox_SEARCH = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usefullShellCommandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tcpvcon64exeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.netdumpexeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.netfilterexeTrueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pEMultiFileScannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLogFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_RUN = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.textBox_Arguments = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView_Spawns = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.contextMenuStrip_RightMouseClickOnDataGridView.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Spawns)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // debugWindow
            // 
            this.debugWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.debugWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debugWindow.Location = new System.Drawing.Point(0, 0);
            this.debugWindow.Name = "debugWindow";
            this.debugWindow.Size = new System.Drawing.Size(732, 140);
            this.debugWindow.TabIndex = 0;
            this.debugWindow.Text = "Bug: When process WMI Provider Host (WmiPrvSE.exe) uses high cpu do in cmd as adm" +
    "in: net stop winmgmt\n\n";
            // 
            // contextMenuStrip_RightMouseClickOnDataGridView
            // 
            this.contextMenuStrip_RightMouseClickOnDataGridView.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip_RightMouseClickOnDataGridView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openPathToolStripMenuItem,
            this.copySelectedValueToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripMenuItem_KillProcess,
            this.toolStripMenuItem_SuspendProcess,
            this.resumeProcessToolStripMenuItem,
            this.toolStripSeparator2,
            this.alwaysAllowThisProcessToolStripMenuItem,
            this.alwaysTerminateThisProcessToolStripMenuItem,
            this.removeFromKnownProcessListToolStripMenuItem});
            this.contextMenuStrip_RightMouseClickOnDataGridView.Name = "contextMenuStrip1";
            this.contextMenuStrip_RightMouseClickOnDataGridView.Size = new System.Drawing.Size(289, 208);
            // 
            // openPathToolStripMenuItem
            // 
            this.openPathToolStripMenuItem.Name = "openPathToolStripMenuItem";
            this.openPathToolStripMenuItem.Size = new System.Drawing.Size(288, 24);
            this.openPathToolStripMenuItem.Text = "Open ExecutablePath";
            this.openPathToolStripMenuItem.Click += new System.EventHandler(this.openPathToolStripMenuItem_Click);
            // 
            // copySelectedValueToolStripMenuItem
            // 
            this.copySelectedValueToolStripMenuItem.Name = "copySelectedValueToolStripMenuItem";
            this.copySelectedValueToolStripMenuItem.Size = new System.Drawing.Size(288, 24);
            this.copySelectedValueToolStripMenuItem.Text = "Copy Selected Value";
            this.copySelectedValueToolStripMenuItem.Click += new System.EventHandler(this.copySelectedValueToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(285, 6);
            // 
            // toolStripMenuItem_KillProcess
            // 
            this.toolStripMenuItem_KillProcess.Name = "toolStripMenuItem_KillProcess";
            this.toolStripMenuItem_KillProcess.Size = new System.Drawing.Size(288, 24);
            this.toolStripMenuItem_KillProcess.Text = "Kill Process";
            this.toolStripMenuItem_KillProcess.Click += new System.EventHandler(this.killProcessToolStripMenuItem_Click);
            // 
            // toolStripMenuItem_SuspendProcess
            // 
            this.toolStripMenuItem_SuspendProcess.Name = "toolStripMenuItem_SuspendProcess";
            this.toolStripMenuItem_SuspendProcess.Size = new System.Drawing.Size(288, 24);
            this.toolStripMenuItem_SuspendProcess.Text = "Suspend Process";
            this.toolStripMenuItem_SuspendProcess.Click += new System.EventHandler(this.suspendProcessToolStripMenuItem_Click);
            // 
            // resumeProcessToolStripMenuItem
            // 
            this.resumeProcessToolStripMenuItem.Name = "resumeProcessToolStripMenuItem";
            this.resumeProcessToolStripMenuItem.Size = new System.Drawing.Size(288, 24);
            this.resumeProcessToolStripMenuItem.Text = "Resume Process";
            this.resumeProcessToolStripMenuItem.Click += new System.EventHandler(this.resumeProcessToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(285, 6);
            // 
            // alwaysAllowThisProcessToolStripMenuItem
            // 
            this.alwaysAllowThisProcessToolStripMenuItem.Name = "alwaysAllowThisProcessToolStripMenuItem";
            this.alwaysAllowThisProcessToolStripMenuItem.Size = new System.Drawing.Size(288, 24);
            this.alwaysAllowThisProcessToolStripMenuItem.Text = "Always allow this process";
            this.alwaysAllowThisProcessToolStripMenuItem.Click += new System.EventHandler(this.alwaysAllowThisProcessToolStripMenuItem_Click);
            // 
            // alwaysTerminateThisProcessToolStripMenuItem
            // 
            this.alwaysTerminateThisProcessToolStripMenuItem.Name = "alwaysTerminateThisProcessToolStripMenuItem";
            this.alwaysTerminateThisProcessToolStripMenuItem.Size = new System.Drawing.Size(288, 24);
            this.alwaysTerminateThisProcessToolStripMenuItem.Text = "Always Terminate this process";
            this.alwaysTerminateThisProcessToolStripMenuItem.Click += new System.EventHandler(this.alwaysTerminateThisProcessToolStripMenuItem_Click);
            // 
            // removeFromKnownProcessListToolStripMenuItem
            // 
            this.removeFromKnownProcessListToolStripMenuItem.Name = "removeFromKnownProcessListToolStripMenuItem";
            this.removeFromKnownProcessListToolStripMenuItem.Size = new System.Drawing.Size(288, 24);
            this.removeFromKnownProcessListToolStripMenuItem.Text = "Remove from KnownProcessList";
            this.removeFromKnownProcessListToolStripMenuItem.Click += new System.EventHandler(this.removeFromKnownProcessListToolStripMenuItem_Click);
            // 
            // textBox_SEARCH
            // 
            this.textBox_SEARCH.Location = new System.Drawing.Point(453, 4);
            this.textBox_SEARCH.Name = "textBox_SEARCH";
            this.textBox_SEARCH.Size = new System.Drawing.Size(267, 22);
            this.textBox_SEARCH.TabIndex = 2;
            this.textBox_SEARCH.TextChanged += new System.EventHandler(this.textBox_SEARCH_TextChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(732, 28);
            this.menuStrip1.Stretch = false;
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usefullShellCommandsToolStripMenuItem,
            this.tcpvcon64exeToolStripMenuItem,
            this.netdumpexeToolStripMenuItem,
            this.netfilterexeTrueToolStripMenuItem,
            this.pEMultiFileScannerToolStripMenuItem});
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(48, 24);
            this.runToolStripMenuItem.Text = "Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // usefullShellCommandsToolStripMenuItem
            // 
            this.usefullShellCommandsToolStripMenuItem.Name = "usefullShellCommandsToolStripMenuItem";
            this.usefullShellCommandsToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.usefullShellCommandsToolStripMenuItem.Text = "Usefull Shell Commands";
            // 
            // tcpvcon64exeToolStripMenuItem
            // 
            this.tcpvcon64exeToolStripMenuItem.Name = "tcpvcon64exeToolStripMenuItem";
            this.tcpvcon64exeToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.tcpvcon64exeToolStripMenuItem.Text = "tcpvcon64.exe";
            this.tcpvcon64exeToolStripMenuItem.Click += new System.EventHandler(this.tcpvcon64exeToolStripMenuItem_Click);
            // 
            // netdumpexeToolStripMenuItem
            // 
            this.netdumpexeToolStripMenuItem.Name = "netdumpexeToolStripMenuItem";
            this.netdumpexeToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.netdumpexeToolStripMenuItem.Text = "netdump.exe";
            this.netdumpexeToolStripMenuItem.Click += new System.EventHandler(this.netdumpexeToolStripMenuItem_ClickAsync);
            // 
            // netfilterexeTrueToolStripMenuItem
            // 
            this.netfilterexeTrueToolStripMenuItem.Name = "netfilterexeTrueToolStripMenuItem";
            this.netfilterexeTrueToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.netfilterexeTrueToolStripMenuItem.Text = "netfilter.exe";
            this.netfilterexeTrueToolStripMenuItem.Click += new System.EventHandler(this.netfilterexeToolStripMenuItem_Click);
            // 
            // pEMultiFileScannerToolStripMenuItem
            // 
            this.pEMultiFileScannerToolStripMenuItem.Name = "pEMultiFileScannerToolStripMenuItem";
            this.pEMultiFileScannerToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.pEMultiFileScannerToolStripMenuItem.Text = "PE Multi File Scanner C#";
            this.pEMultiFileScannerToolStripMenuItem.Click += new System.EventHandler(this.pEMultiFileScannerToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filtersToolStripMenuItem,
            this.openLogFileToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(75, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // filtersToolStripMenuItem
            // 
            this.filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            this.filtersToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.filtersToolStripMenuItem.Text = "Preferences";
            this.filtersToolStripMenuItem.Click += new System.EventHandler(this.filtersToolStripMenuItem_Click);
            // 
            // openLogFileToolStripMenuItem
            // 
            this.openLogFileToolStripMenuItem.Name = "openLogFileToolStripMenuItem";
            this.openLogFileToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.openLogFileToolStripMenuItem.Text = "Open Log File";
            this.openLogFileToolStripMenuItem.Click += new System.EventHandler(this.openLogFileToolStripMenuItem_Click);
            // 
            // contextMenuStrip_RUN
            // 
            this.contextMenuStrip_RUN.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip_RUN.Name = "contextMenuStrip1";
            this.contextMenuStrip_RUN.Size = new System.Drawing.Size(61, 4);
            // 
            // textBox_Arguments
            // 
            this.textBox_Arguments.Location = new System.Drawing.Point(280, 4);
            this.textBox_Arguments.Name = "textBox_Arguments";
            this.textBox_Arguments.Size = new System.Drawing.Size(97, 22);
            this.textBox_Arguments.TabIndex = 4;
            this.textBox_Arguments.Text = "true";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView_Spawns);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.debugWindow);
            this.splitContainer1.Size = new System.Drawing.Size(732, 299);
            this.splitContainer1.SplitterDistance = 151;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 5;
            // 
            // dataGridView_Spawns
            // 
            this.dataGridView_Spawns.AllowUserToAddRows = false;
            this.dataGridView_Spawns.AllowUserToDeleteRows = false;
            this.dataGridView_Spawns.AllowUserToOrderColumns = true;
            this.dataGridView_Spawns.AllowUserToResizeRows = false;
            this.dataGridView_Spawns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Spawns.ContextMenuStrip = this.contextMenuStrip_RightMouseClickOnDataGridView;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_Spawns.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_Spawns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Spawns.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_Spawns.Name = "dataGridView_Spawns";
            this.dataGridView_Spawns.RowHeadersWidth = 51;
            this.dataGridView_Spawns.RowTemplate.Height = 24;
            this.dataGridView_Spawns.Size = new System.Drawing.Size(732, 151);
            this.dataGridView_Spawns.TabIndex = 1;
            this.dataGridView_Spawns.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Spawns_CellContentClick);
            this.dataGridView_Spawns.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_Spawns_CellMouseDown);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 327);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(732, 26);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(151, 20);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 18);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(732, 353);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.textBox_Arguments);
            this.Controls.Add(this.textBox_SEARCH);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.contextMenuStrip_RightMouseClickOnDataGridView.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Spawns)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox debugWindow;
        private System.Windows.Forms.TextBox textBox_SEARCH;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_RightMouseClickOnDataGridView;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_KillProcess;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_SuspendProcess;
        private System.Windows.Forms.ToolStripMenuItem resumeProcessToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tcpvcon64exeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_RUN;
        private System.Windows.Forms.ToolStripMenuItem netdumpexeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem netfilterexeTrueToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox_Arguments;
        private System.Windows.Forms.ToolStripMenuItem usefullShellCommandsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pEMultiFileScannerToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView_Spawns;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripMenuItem openPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySelectedValueToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtersToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem alwaysAllowThisProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alwaysTerminateThisProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFromKnownProcessListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLogFileToolStripMenuItem;
    }
}

