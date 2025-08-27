using System.Drawing;
using System.Windows.Forms;

namespace LADXHD_Patcher
{
    internal class Forms
    {
        public static Form_MainForm  mainDialog;
        public static Form_OkayForm  okayDialog; 
        public static Form_YesNoForm yesNoDialog; 

        public static void Initialize()
        {
            mainDialog  = new Form_MainForm();
            okayDialog  = new Form_OkayForm();
            yesNoDialog = new Form_YesNoForm();
        }

        public static void CreatePatcherText()
        {
            // Set the title including the version number.
            mainDialog.Text = "Link's Awakening DX HD Patcher v" + Config.version;

            // A transparent label is used to overlap the Advanced Rich Textbox so it can't be interacted with.
            mainDialog.TextBox_NoClick = new TransparentLabel();
            mainDialog.TextBox_NoClick.Text = "";
            mainDialog.TextBox_NoClick.Size = new Size(307, 100);
            mainDialog.TextBox_NoClick.Location = new Point(13, 133);
            mainDialog.TextBox_NoClick.TabIndex = 16;
            mainDialog.Controls.Add(mainDialog.TextBox_NoClick);

            // The Advanced RichTextBox allows for justified text.
            mainDialog.TextBox_Info = new AdvRichTextBox();

            // Set the text of the textbox.
            mainDialog.TextBox_Info.Text = "" +
            "Patches v1.0.0 (or v1.1.4+) to v" + Config.version + " with the \"Patch\" button " +
            "below. All patchers created since v1.1.4 back up the original " +
            "files so future  patches no longer require v1.0.0. When updating " +
            "with this version of the patcher, future versions of the " +
            "patcher can use the stored backup files. Backups are stored in the " +
            "\"Data\\Backup\" folder. Do not move or delete them! ";

            // Justification must be done after the text has been added for it to take effect.
            mainDialog.TextBox_Info.SelectionAlignment = TextAlign.Justify;
            mainDialog.TextBox_Info.Size = new Size(307, 100);
            mainDialog.TextBox_Info.Location = new Point(13, 133);
            mainDialog.TextBox_Info.TabStop = false;
            mainDialog.TextBox_Info.BorderStyle = BorderStyle.None;
            mainDialog.TextBox_Info.ReadOnly = true;
            mainDialog.TextBox_Info.BackColor = ColorTranslator.FromHtml("#F0F0F0");
            mainDialog.Controls.Add(mainDialog.TextBox_Info);
        }
    }
}
