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
            mainDialog.TextBox_NoClick.Size = new Size(306, 100);
            mainDialog.TextBox_NoClick.Location = new Point(13, 133);
            mainDialog.TextBox_NoClick.TabIndex = 16;
            mainDialog.Controls.Add(mainDialog.TextBox_NoClick);

            // The Advanced RichTextBox allows for justified text.
            mainDialog.TextBox_Info = new AdvRichTextBox();

            // Set the text of the textbox.
            mainDialog.TextBox_Info.Text = "" +
            "Before you begin, make sure to create a backup of the game as " +
            "it will be required for future patches. This program can patch the " +
            "v1.0.0 release of \"The Legend of Zelda: Link's Awakening DX " +
            "HD\" to v" + Config.version + " using a collection of built-in xdelta3 patches. The " +
            "game is patched \"in-place\" meaning the original version will be " +
            "lost. Simply press the \"Patch\" button and wait for it to finish.";

            // Justification must be done after the text has been added for it to take effect.
            mainDialog.TextBox_Info.SelectionAlignment = TextAlign.Justify;
            mainDialog.TextBox_Info.Size = new Size(306, 100);
            mainDialog.TextBox_Info.Location = new Point(12, 132);
            mainDialog.TextBox_Info.TabStop = false;
            mainDialog.TextBox_Info.BorderStyle = BorderStyle.None;
            mainDialog.TextBox_Info.ReadOnly = true;
            mainDialog.TextBox_Info.BackColor = ColorTranslator.FromHtml("#F0F0F0");
            mainDialog.Controls.Add(mainDialog.TextBox_Info);
        }
    }
}
