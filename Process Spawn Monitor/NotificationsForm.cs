using System;
using System.Drawing;
using System.Windows.Forms;

namespace Process_Spawn_Monitor
{
    public partial class NotificationsForm : Form
    {
        public NotificationsForm()
        {
            InitializeComponent();

            Screen screen = Screen.PrimaryScreen;
            Rectangle workingArea = screen.WorkingArea;
            this.Location = new Point(workingArea.Right - this.Width, workingArea.Bottom - this.Height);
        }
        public void UpdateNotificationsWindow(string message)
        {
            if (richTextBox_Notifications.InvokeRequired)
            {
                richTextBox_Notifications.Invoke(new Action(() =>
                {
                    richTextBox_Notifications.AppendText(message + "\n");
                    this.Show();
                    //this.Show();
                    //this.BringToFront();
                    //this.TopMost = true;
                    //this.Visible = true;
                }));
            }
            else
            {
                richTextBox_Notifications.AppendText(message + "\n");
            }
        }
        private void richTextBox_Notifications_TextChanged(object sender, EventArgs e)
        {
            richTextBox_Notifications.Show();
            richTextBox_Notifications.ScrollToCaret();
            this.BringToFront();
            this.TopMost = true;        // Toggle to show notification. Only this works
            this.TopMost = false;
        }

        private void richTextBox_Notifications_MouseEnter(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
    public class Notifications
    {
        public static bool CheckNotificationsEnabled()
        {
            if (IniFileManager.GetKeyValueBool("ProcessMonitor", "DisplayNotifications"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

