namespace Process_Spawn_Monitor
{
    partial class Preferences
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OnNewDetectedUnlistedProcess = new System.Windows.Forms.GroupBox();
            this.DoNothing = new System.Windows.Forms.RadioButton();
            this.Ask = new System.Windows.Forms.RadioButton();
            this.SuspendAndAsk = new System.Windows.Forms.RadioButton();
            this.Terminate = new System.Windows.Forms.RadioButton();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_ok = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox_DisplayNitifications = new System.Windows.Forms.CheckBox();
            this.TerminateOnlyKnownLeaveOthers = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.OnNewDetectedUnlistedProcess.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OnNewDetectedUnlistedProcess);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(282, 192);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Process Manager";
            // 
            // OnNewDetectedUnlistedProcess
            // 
            this.OnNewDetectedUnlistedProcess.Controls.Add(this.TerminateOnlyKnownLeaveOthers);
            this.OnNewDetectedUnlistedProcess.Controls.Add(this.DoNothing);
            this.OnNewDetectedUnlistedProcess.Controls.Add(this.Ask);
            this.OnNewDetectedUnlistedProcess.Controls.Add(this.SuspendAndAsk);
            this.OnNewDetectedUnlistedProcess.Controls.Add(this.Terminate);
            this.OnNewDetectedUnlistedProcess.Location = new System.Drawing.Point(6, 21);
            this.OnNewDetectedUnlistedProcess.Name = "OnNewDetectedUnlistedProcess";
            this.OnNewDetectedUnlistedProcess.Size = new System.Drawing.Size(266, 162);
            this.OnNewDetectedUnlistedProcess.TabIndex = 3;
            this.OnNewDetectedUnlistedProcess.TabStop = false;
            this.OnNewDetectedUnlistedProcess.Text = "On new detected unlisted process";
            // 
            // DoNothing
            // 
            this.DoNothing.AutoSize = true;
            this.DoNothing.Checked = true;
            this.DoNothing.Location = new System.Drawing.Point(6, 130);
            this.DoNothing.Name = "DoNothing";
            this.DoNothing.Size = new System.Drawing.Size(92, 20);
            this.DoNothing.TabIndex = 7;
            this.DoNothing.TabStop = true;
            this.DoNothing.Text = "Do nothing";
            this.DoNothing.UseVisualStyleBackColor = true;
            // 
            // Ask
            // 
            this.Ask.AutoSize = true;
            this.Ask.Location = new System.Drawing.Point(6, 104);
            this.Ask.Name = "Ask";
            this.Ask.Size = new System.Drawing.Size(51, 20);
            this.Ask.TabIndex = 6;
            this.Ask.Text = "Ask";
            this.Ask.UseVisualStyleBackColor = true;
            // 
            // SuspendAndAsk
            // 
            this.SuspendAndAsk.AutoSize = true;
            this.SuspendAndAsk.Location = new System.Drawing.Point(6, 78);
            this.SuspendAndAsk.Name = "SuspendAndAsk";
            this.SuspendAndAsk.Size = new System.Drawing.Size(134, 20);
            this.SuspendAndAsk.TabIndex = 5;
            this.SuspendAndAsk.Text = "Suspend and Ask";
            this.SuspendAndAsk.UseVisualStyleBackColor = true;
            // 
            // Terminate
            // 
            this.Terminate.AutoSize = true;
            this.Terminate.Location = new System.Drawing.Point(6, 26);
            this.Terminate.Name = "Terminate";
            this.Terminate.Size = new System.Drawing.Size(89, 20);
            this.Terminate.TabIndex = 4;
            this.Terminate.Text = "Terminate";
            this.Terminate.UseVisualStyleBackColor = true;
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(445, 285);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 2;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(364, 285);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 1;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Location = new System.Drawing.Point(300, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(251, 170);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Process Monitor";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox_DisplayNitifications);
            this.groupBox3.Location = new System.Drawing.Point(6, 21);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(237, 60);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Notifications";
            // 
            // checkBox_DisplayNitifications
            // 
            this.checkBox_DisplayNitifications.AutoSize = true;
            this.checkBox_DisplayNitifications.Checked = true;
            this.checkBox_DisplayNitifications.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_DisplayNitifications.Location = new System.Drawing.Point(6, 27);
            this.checkBox_DisplayNitifications.Name = "checkBox_DisplayNitifications";
            this.checkBox_DisplayNitifications.Size = new System.Drawing.Size(151, 20);
            this.checkBox_DisplayNitifications.TabIndex = 0;
            this.checkBox_DisplayNitifications.Text = "Display Notifications";
            this.checkBox_DisplayNitifications.UseVisualStyleBackColor = true;
            // 
            // TerminateOnlyKnownLeaveOthers
            // 
            this.TerminateOnlyKnownLeaveOthers.AutoSize = true;
            this.TerminateOnlyKnownLeaveOthers.Location = new System.Drawing.Point(6, 52);
            this.TerminateOnlyKnownLeaveOthers.Name = "TerminateOnlyKnownLeaveOthers";
            this.TerminateOnlyKnownLeaveOthers.Size = new System.Drawing.Size(247, 20);
            this.TerminateOnlyKnownLeaveOthers.TabIndex = 8;
            this.TerminateOnlyKnownLeaveOthers.Text = "Terminate Only Known, Leave Others";
            this.TerminateOnlyKnownLeaveOthers.UseVisualStyleBackColor = true;
            // 
            // Preferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 320);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.groupBox1);
            this.Name = "Preferences";
            this.Text = "Preferences";
            this.groupBox1.ResumeLayout(false);
            this.OnNewDetectedUnlistedProcess.ResumeLayout(false);
            this.OnNewDetectedUnlistedProcess.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox OnNewDetectedUnlistedProcess;
        private System.Windows.Forms.RadioButton Ask;
        private System.Windows.Forms.RadioButton SuspendAndAsk;
        private System.Windows.Forms.RadioButton Terminate;
        private System.Windows.Forms.RadioButton DoNothing;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBox_DisplayNitifications;
        private System.Windows.Forms.RadioButton TerminateOnlyKnownLeaveOthers;
    }
}