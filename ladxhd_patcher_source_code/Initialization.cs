using System;
using System.Windows.Forms;

namespace LADXHD_Patcher
{
    internal static class Initialization
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize the classes.
            Forms.Initialize();
            Config.Initialize();
            XDelta3.Initialize();

            // Show the main dialog.
            Forms.mainDialog.ShowDialog();
        }
    }
}
