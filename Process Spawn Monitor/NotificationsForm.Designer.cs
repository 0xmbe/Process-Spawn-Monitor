namespace Process_Spawn_Monitor
{
    partial class NotificationsForm
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
            this.richTextBox_Notifications = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBox_Notifications
            // 
            this.richTextBox_Notifications.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.richTextBox_Notifications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_Notifications.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.richTextBox_Notifications.Location = new System.Drawing.Point(0, 0);
            this.richTextBox_Notifications.Name = "richTextBox_Notifications";
            this.richTextBox_Notifications.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox_Notifications.Size = new System.Drawing.Size(189, 90);
            this.richTextBox_Notifications.TabIndex = 0;
            this.richTextBox_Notifications.Text = "";
            this.richTextBox_Notifications.TextChanged += new System.EventHandler(this.richTextBox_Notifications_TextChanged);
            this.richTextBox_Notifications.MouseEnter += new System.EventHandler(this.richTextBox_Notifications_MouseEnter);
            // 
            // NotificationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(189, 90);
            this.Controls.Add(this.richTextBox_Notifications);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NotificationsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NotificationsForm";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox richTextBox_Notifications;
    }
}