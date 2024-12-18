using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using IniParser;
using IniParser.Model;

namespace Process_Spawn_Monitor
{
    public partial class Preferences : Form
    {
        public Preferences()
        {
            InitializeComponent();

            LoadIniSettings();
        }

        private void LoadIniSettings()                                      // LOAD SETTINGS
        {
            var data = IniFileManager.LoadIniFile(IniFileManager.iniFileName);
            GetRadioButton(OnNewDetectedUnlistedProcess, data["ProcessManager"]["OnNewDetectedUnlistedProcess"]).Checked = true;

            // Make this automatic for all group boxes
        }
        private void button_ok_Click(object sender, EventArgs e)            // SAVE SETTINGS
        {
            var parser = new FileIniDataParser();
            var data = IniFileManager.LoadIniFile(IniFileManager.iniFileName);

            data["ProcessManager"]["OnNewDetectedUnlistedProcess"] = GetSelectedRadioButtonName(OnNewDetectedUnlistedProcess);

            // Make this automatic for all group boxes


            parser.WriteFile(IniFileManager.iniFileName, data);
            this.Close();
        }
        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string GetSelectedRadioButtonName(GroupBox groupBox)
        {
            foreach (Control control in groupBox.Controls)
            {
                if (control is RadioButton)
                {
                    RadioButton radioButton = (RadioButton)control;
                    if (radioButton.Checked)
                    {
                        return radioButton.Name;
                    }
                }
            }
            return "";
        }
        private RadioButton GetRadioButton(GroupBox groupBox, string radioButtonName)
        {
            foreach (Control control in groupBox.Controls)
            {
                if (control is RadioButton)
                {
                    RadioButton radioButton = (RadioButton)control;
                    if (radioButton.Name == radioButtonName)
                    {
                        return radioButton;
                    }
                }
            }
            return null;
        }

    }
}
