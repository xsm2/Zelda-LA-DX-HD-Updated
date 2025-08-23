using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LADXHD_Patcher
{
    public partial class Form_MainForm : Form
    {
        public Form_MainForm()
        {
            InitializeComponent();
        }

        public void ToggleDialog(bool toggle)
        {
            button_Patch.Enabled = toggle;
            button_ChangeLog.Enabled = toggle;
            button_Exit.Enabled = toggle;
        }

        private void button_Patch_Click(object sender, EventArgs e)
        {
            if (Config.Validate())
                Functions.StartPatching();
        }

        private void button_ChangeLog_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/BigheadSMZ/Zelda-LA-DX-HD-Updated/blob/main/CHANGELOG.md");
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
