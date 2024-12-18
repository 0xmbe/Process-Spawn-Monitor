
namespace Process_Spawn_Monitor
{
    partial class NewProcessPopup
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
            this.button_AllowThisTimeOnly = new System.Windows.Forms.Button();
            this.button_SuspendThisTimeOnly = new System.Windows.Forms.Button();
            this.button_SuspendAlways = new System.Windows.Forms.Button();
            this.button_AllowAlways = new System.Windows.Forms.Button();
            this.button_TerminateThisTimeOnly = new System.Windows.Forms.Button();
            this.button_TerminateAlways = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_AllowThisTimeOnly
            // 
            this.button_AllowThisTimeOnly.Location = new System.Drawing.Point(432, 174);
            this.button_AllowThisTimeOnly.Name = "button_AllowThisTimeOnly";
            this.button_AllowThisTimeOnly.Size = new System.Drawing.Size(204, 32);
            this.button_AllowThisTimeOnly.TabIndex = 3;
            this.button_AllowThisTimeOnly.Text = "Allow This Time Only";
            this.button_AllowThisTimeOnly.UseVisualStyleBackColor = true;
            this.button_AllowThisTimeOnly.Click += new System.EventHandler(this.button_AllowThisTimeOnly_Click);
            // 
            // button_SuspendThisTimeOnly
            // 
            this.button_SuspendThisTimeOnly.Location = new System.Drawing.Point(222, 174);
            this.button_SuspendThisTimeOnly.Name = "button_SuspendThisTimeOnly";
            this.button_SuspendThisTimeOnly.Size = new System.Drawing.Size(204, 32);
            this.button_SuspendThisTimeOnly.TabIndex = 5;
            this.button_SuspendThisTimeOnly.Text = "Suspend This Time Only";
            this.button_SuspendThisTimeOnly.UseVisualStyleBackColor = true;
            this.button_SuspendThisTimeOnly.Click += new System.EventHandler(this.button_SuspendThisTimeOnly_Click);
            // 
            // button_SuspendAlways
            // 
            this.button_SuspendAlways.Location = new System.Drawing.Point(222, 212);
            this.button_SuspendAlways.Name = "button_SuspendAlways";
            this.button_SuspendAlways.Size = new System.Drawing.Size(204, 32);
            this.button_SuspendAlways.TabIndex = 6;
            this.button_SuspendAlways.Text = "Suspend Always";
            this.button_SuspendAlways.UseVisualStyleBackColor = true;
            this.button_SuspendAlways.Click += new System.EventHandler(this.button_SuspendAlways_Click);
            // 
            // button_AllowAlways
            // 
            this.button_AllowAlways.Location = new System.Drawing.Point(432, 212);
            this.button_AllowAlways.Name = "button_AllowAlways";
            this.button_AllowAlways.Size = new System.Drawing.Size(204, 32);
            this.button_AllowAlways.TabIndex = 7;
            this.button_AllowAlways.Text = "Allow Always";
            this.button_AllowAlways.UseVisualStyleBackColor = true;
            this.button_AllowAlways.Click += new System.EventHandler(this.button_AllowAlways_Click);
            // 
            // button_TerminateThisTimeOnly
            // 
            this.button_TerminateThisTimeOnly.Location = new System.Drawing.Point(12, 174);
            this.button_TerminateThisTimeOnly.Name = "button_TerminateThisTimeOnly";
            this.button_TerminateThisTimeOnly.Size = new System.Drawing.Size(204, 32);
            this.button_TerminateThisTimeOnly.TabIndex = 8;
            this.button_TerminateThisTimeOnly.Text = "Terminate This Time Only";
            this.button_TerminateThisTimeOnly.UseVisualStyleBackColor = true;
            this.button_TerminateThisTimeOnly.Click += new System.EventHandler(this.button_TerminateThisTimeOnly_Click);
            // 
            // button_TerminateAlways
            // 
            this.button_TerminateAlways.Location = new System.Drawing.Point(12, 212);
            this.button_TerminateAlways.Name = "button_TerminateAlways";
            this.button_TerminateAlways.Size = new System.Drawing.Size(204, 32);
            this.button_TerminateAlways.TabIndex = 9;
            this.button_TerminateAlways.Text = "Terminate Always";
            this.button_TerminateAlways.UseVisualStyleBackColor = true;
            this.button_TerminateAlways.Click += new System.EventHandler(this.button_TerminateAlways_Click);
            // 
            // NewProcessPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 253);
            this.Controls.Add(this.button_TerminateAlways);
            this.Controls.Add(this.button_TerminateThisTimeOnly);
            this.Controls.Add(this.button_AllowAlways);
            this.Controls.Add(this.button_SuspendAlways);
            this.Controls.Add(this.button_SuspendThisTimeOnly);
            this.Controls.Add(this.button_AllowThisTimeOnly);
            this.MaximizeBox = false;
            this.Name = "NewProcessPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NewProcessPopup";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button_AllowThisTimeOnly;
        private System.Windows.Forms.Button button_SuspendThisTimeOnly;
        private System.Windows.Forms.Button button_SuspendAlways;
        private System.Windows.Forms.Button button_AllowAlways;
        private System.Windows.Forms.Button button_TerminateThisTimeOnly;
        private System.Windows.Forms.Button button_TerminateAlways;
    }
}