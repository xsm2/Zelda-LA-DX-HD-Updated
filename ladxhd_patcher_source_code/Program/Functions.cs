using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace LADXHD_Patcher
{
    internal class Functions
    {
        private static bool patchFromBackup;
        private static Dictionary<string, object> resources = ResourceHelper.GetAllResources();
        private static string[] languageFiles  = new[] { "esp", "fre", "ita", "por", "rus" };
        private static string[] languageDialog = new[] { "dialog_esp", "dialog_fre", "dialog_ita", "dialog_por", "dialog_rus" };

        public static string CalculateHash(string FilePath, string HashType)
        {
            HashAlgorithm Algorithm = HashAlgorithm.Create(HashType);
            byte[] ByteArray = File.ReadAllBytes(FilePath);
            return BitConverter.ToString(Algorithm.ComputeHash(ByteArray)).Replace("-", "");
        }

        private static void LanguagePatches(FileItem fileItem)
        {
            // Get the kind of patch file we want to create.
            string[] target = null;
            if (fileItem.Name == "eng.lng")
                target = languageFiles;
            else if (fileItem.Name == "dialog_eng.lng")
                target = languageDialog;

            if (target == null) return;

            foreach (string lang in target)
            {
                // Create the xdelta patch file.
                string langFile = lang + ".lng";
                string xdelta3File = Path.Combine((Config.tempFolder + "\\patches").CreatePath(), langFile + ".xdelta");
                File.WriteAllBytes(xdelta3File, (byte[])resources[langFile]);

                // Patch the english file to get the new language file.
                string patchedFile = Path.Combine((Config.tempFolder + "\\patchedFiles").CreatePath(), langFile);
                XDelta3.Args = XDelta3.GetApplyArguments(fileItem.FullName, xdelta3File, patchedFile);
                XDelta3.Start();

                // Move the newly created language file to where the english file was found.
                string TargetPath = Path.Combine(fileItem.DirectoryName, langFile);
                patchedFile.MovePath(TargetPath, false);
            }
        }

        private static void PatchGameFiles()
        {
            File.WriteAllBytes(XDelta3.Exe, (byte[])resources["xdelta3.exe"]);

            foreach (string file in Config.baseFolder.GetFiles("*", true))
            {
                // My "fileItem" class is great for grabbing specific file/folder information.
                FileItem fileItem = new FileItem(file);
                FileItem folderItem = new FileItem(fileItem.DirectoryName);

                // We skip backup files and languages other than english here as they are patched from english files. We
                // also skip the xdelta3 executable for obvious reasons, and we skip the file if there is no patch for it.
                if (languageFiles.Contains(fileItem.BaseName) || languageDialog.Contains(fileItem.BaseName) ||
                    folderItem.Name == "Backup" || fileItem.Name == "xdelta3.exe" || !resources.ContainsKey(fileItem.Name) )
                    continue;

                // If a backup file exists, restore it. If it doesn't exist, create one.
                string backupFile = Path.Combine(Config.backupPath, fileItem.Name);
                if (backupFile.TestPath())
                    backupFile.CopyPath(fileItem.FullName,true);
                else
                    fileItem.FullName.CopyPath(backupFile, true);

                // When we find english files, run a sub-routine to create other language files.
                if (fileItem.Name == "eng.lng" || fileItem.Name == "dialog_eng.lng")
                    LanguagePatches(fileItem);

                // Create the xdelta patch file.
                string xdelta3File = Path.Combine((Config.tempFolder + "\\patches").CreatePath(), fileItem.Name + ".xdelta");
                File.WriteAllBytes(xdelta3File, (byte[])resources[fileItem.Name]);

                // Patch the file and overwrite the original file with it.
                string patchedFile = Path.Combine((Config.tempFolder + "\\patchedFiles").CreatePath(), fileItem.Name);
                XDelta3.Args = XDelta3.GetApplyArguments(fileItem.FullName, xdelta3File, patchedFile);
                XDelta3.Start();
                patchedFile.MovePath(fileItem.FullName, true);
            }
            string message = patchFromBackup 
                ? "Patching the game from v1.0.0 backup files was successful. The game was updated to v"+ Config.version + "." 
                : "Patching Link's Awakening DX HD v1.0.0 was successful. The game was updated to v"+ Config.version + ".";
            Forms.okayDialog.Display("Patching Complete", 260, 40, 34, 16, 10, message);
        }

        public static bool ValidateStart()
        {
            // Check for the game executable existence.
            if (!Config.zeldaEXE.TestPath())
            {
                Forms.okayDialog.Display("Game Executable Not Found", 250, 40, 27, 10, 15, 
                    "Could not find \"Link's Awakening DX HD.exe\" to patch. Copy this patcher executable to the folder of the original release of v1.0.0 and run it from there.");
                return false;
            }
            // Get the MD5 hash of the executable.
            string md5Hash = Functions.CalculateHash(Config.zeldaEXE, "MD5");

            // Game has already been patched.
            if (md5Hash == Config.newHash)
            {
                Forms.okayDialog.Display("Already Patched", 260, 40, 30, 16, 10, 
                    "The game is already at v" + Config.version + " so no patching is needed. Close this patcher and launch the game!");
                return false;
            }
            // Check for a backup folder. If executable is there, get hash of that instead.
            if (!Config.backupPath.IsPathEmpty())
            {
                string backupExe = Path.Combine(Config.backupPath,"Link's Awakening DX HD.exe");
                if (backupExe.TestPath())
                {
                    md5Hash = Functions.CalculateHash(backupExe, "MD5");
                    patchFromBackup = true;
                }
            }
            // Unknown version of the game.
            if (md5Hash != Config.oldHash && md5Hash != Config.newHash)
            {
                Forms.okayDialog.Display("Uknown Version", 260, 40, 26, 24, 10, 
                    "The version you are attempting to patch is unknown!");
                return false;
            }
            // If the executable is v1.0.0 then start patching.
            if (md5Hash == Config.oldHash)
                return true;

            // Something unexpected has gone wrong.
            Forms.okayDialog.Display("Unknown Error", 260, 40, 30, 16, 10, 
                "Something unexpected went wrong. Make sure the game is actually the original v1.0.0 release.");
            return false;
        }

        private static bool ValidateInput()
        {
            return Forms.yesNoDialog.Display("Patch v1.0.0 to " + Config.version, 260, 20, 28, 24, true, 
                "Are you sure you wish to patch the game to v" + Config.version + "?");
        }

        public static void StartPatching()
        {
            if (!ValidateStart()) return;
            if (!ValidateInput()) return;

            Forms.mainDialog.ToggleDialog(false);
            Config.tempFolder.CreatePath(true);

            PatchGameFiles();

            Config.tempFolder.RemovePath();
            Forms.mainDialog.ToggleDialog(true);
        }
    }
}
