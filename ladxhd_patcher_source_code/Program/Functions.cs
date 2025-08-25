using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace LADXHD_Patcher
{
    internal class Functions
    {

        private static string[] languageFiles  = new[] { "esp", "ita", "por", "rus" };
        private static string[] languageDialog = new[] { "dialog_esp", "dialog_ita", "dialog_por", "dialog_rus" };

        public static string CalculateHash(string FilePath, string HashType)
        {
            HashAlgorithm Algorithm = HashAlgorithm.Create(HashType);
            byte[] ByteArray = File.ReadAllBytes(FilePath);
            return BitConverter.ToString(Algorithm.ComputeHash(ByteArray)).Replace("-", "");
        }

        private static void LanguagePatches(string input)
        {
            FileItem fileItem = new FileItem(input);
            var resources = ResourceHelper.GetAllResources();

            string[] target = null;
            if (fileItem.Name == "eng.lng")
                target = languageFiles;
            else if (fileItem.Name == "dialog_eng.lng")
                target = languageDialog;

            if (target == null) return;

            foreach (string lang in target)
            {
                string xdelta3File = (Config.tempFolder + "\\patches").CreatePath() + "\\" + lang + ".lng.xdelta";
                string patchedFile = (Config.tempFolder + "\\patchedFiles").CreatePath() + "\\" + lang + ".lng";

                string resourceEntry = lang + ".lng";
                File.WriteAllBytes(xdelta3File, (byte[])resources[resourceEntry]);

                XDelta3.Args = XDelta3.GetApplyArguments(fileItem.FullName, xdelta3File, patchedFile);
                XDelta3.Start();

                string TargetPath = fileItem.DirectoryName + "\\" + lang + ".lng";
                patchedFile.MovePath(TargetPath, false);
            }
        }

        private static void PatchGameFiles()
        {
            var resources = ResourceHelper.GetAllResources();
            File.WriteAllBytes(XDelta3.Exe, (byte[])resources["xdelta3.exe"]);

            foreach (string file in Config.baseFolder.GetFiles("*", true))
            {
                FileItem fileItem = new FileItem(file);

                if (languageFiles.Contains(fileItem.BaseName) || languageDialog.Contains(fileItem.BaseName) ||
                    fileItem.Name == "xdelta3.exe" || !resources.ContainsKey(fileItem.Name))
                    continue;

                if (fileItem.Name == "eng.lng" || fileItem.Name == "dialog_eng.lng")
                    LanguagePatches(fileItem.FullName);

                string xdelta3File = (Config.tempFolder + "\\patches").CreatePath() + "\\" + fileItem.Name + ".xdelta";
                string patchedFile = (Config.tempFolder + "\\patchedFiles").CreatePath() + "\\" + fileItem.Name;

                File.WriteAllBytes(xdelta3File, (byte[])resources[fileItem.Name]);

                XDelta3.Args = XDelta3.GetApplyArguments(fileItem.FullName, xdelta3File, patchedFile);
                XDelta3.Start();

                patchedFile.MovePath(fileItem.FullName, true);
            }
            Forms.okayDialog.Display("Patching Complete", 260, 40, 34, 16, 10, 
                "Patching Link's Awakening DX HD v1.0.0 was successful. The game was updated to v" + Config.version + ".");
        }

        private static bool FinalVerify()
        {
            return Forms.yesNoDialog.Display("Patch v1.0.0 to " + Config.version, 260, 40, 28, 16, true, 
                "Are you sure you wish to patch the game to v" + Config.version + "? Make sure you have a backup before continuing.");
        }

        public static void StartPatching()
        {
            if (!FinalVerify()) return;

            Forms.mainDialog.ToggleDialog(false);
            Config.tempFolder.CreatePath(true);

            PatchGameFiles();

            Config.tempFolder.RemovePath();
            Forms.mainDialog.ToggleDialog(true);
        }
    }
}
