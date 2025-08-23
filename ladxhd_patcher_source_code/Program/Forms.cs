namespace LADXHD_Patcher
{
    internal class Forms
    {
        public static Form_MainForm mainDialog;
        public static Form_OkayForm okayDialog; 

        public static void Initialize()
        {
            mainDialog = new Form_MainForm();
            okayDialog = new Form_OkayForm();

            mainDialog.Text = "Link's Awakening DX HD Patcher v" + Config.version;

            mainDialog.Label_Info.Text = "The purpose of this patcher is to update the original release of " +
                                         "The Legend of Zelda: Link's Awakening DX HD to the latest " +
                                         "version. This patcher updates the game to v" + Config.version + ". If not done " +
                                         "yet, drop it into the v1.0.0 folder and press the \"Patch\" button.";
        }
    }
}
