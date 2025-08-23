using System;
using System.IO;
using System.Security.Cryptography;

namespace LADXHD_Patcher
{
    internal class Functions
    {
        public static string CalculateHash(string FilePath, string HashType)
        {
            HashAlgorithm Algorithm = HashAlgorithm.Create(HashType);
            byte[] ByteArray = File.ReadAllBytes(FilePath);
            return BitConverter.ToString(Algorithm.ComputeHash(ByteArray)).Replace("-", "");
        }

        public static void PatchGameFiles()
        {
            var resources = ResourceHelper.GetAllResources();
            File.WriteAllBytes(XDelta3.Exe, (byte[])resources["xdelta3.exe"]);

            foreach (string file in Config.baseFolder.GetFiles("*", true))
            {
                FileItem fileItem = new FileItem(file);

                if (fileItem.Name == "xdelta3.exe" || !resources.ContainsKey(fileItem.Name))
                    continue;

                string xdelta3File = (Config.tempFolder + "\\patches").CreatePath() + "\\" + fileItem.Name + ".xdelta";
                string patchedFile = (Config.tempFolder + "\\patchedFiles").CreatePath() + "\\" + fileItem.Name;

                File.WriteAllBytes(xdelta3File, (byte[])resources[fileItem.Name]);

                XDelta3.Args = XDelta3.GetApplyArguments(fileItem.FullName, xdelta3File, patchedFile);
                XDelta3.Start();

                patchedFile.MovePath(fileItem.FullName, true);
            }
            string Title = "Patching Complete";
            string Message = "Patching Link's Awakening DX HD v1.0.0 was successful. The game was updated to v" + Config.version + ".";
            Forms.okayDialog.Display(Title, Message, 260, 40, 34, 16, 10);
        }

        public static void StartPatching()
        {
            Forms.mainDialog.ToggleDialog(false);
            Config.tempFolder.CreatePath(true);

            PatchGameFiles();

            Config.tempFolder.RemovePath();
            Forms.mainDialog.ToggleDialog(true);
        }
    }
}
