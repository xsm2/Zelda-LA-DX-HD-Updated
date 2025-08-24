using System.IO;
using System.Reflection;

namespace LADXHD_Patcher
{
    internal class Config
    {
        // The hash for "newHash" will need to be calculated for each new version.

        public const string version = "1.1.2";
        public const string oldHash = "F4ADFBA864B852908705EA6A18A48F18";
        public const string newHash = "E8BD8E002AB6A4A34C900211DCAB7EB8";

        public static string appPath;
        public static string baseFolder;
        public static string tempFolder;
        public static string zeldaEXE;

        public static void Initialize()
        {
            appPath = Assembly.GetExecutingAssembly().Location;
            baseFolder = Path.GetDirectoryName(appPath);
            tempFolder = baseFolder + "\\~temp";
            zeldaEXE = baseFolder + "\\Link's Awakening DX HD.exe";
        }

        public static bool Validate()
        {
            if (!zeldaEXE.TestPath())
            {
                Forms.okayDialog.Display("Game Executable Not Found", 250, 40, 27, 10, 15, 
                    "Could not find \"Link's Awakening DX HD.exe\" to patch. Copy this patcher executable to the folder of the original release of v1.0.0 and run it from there.");
                return false;
            }
            string md5Hash = Functions.CalculateHash(zeldaEXE, "MD5");

            if (md5Hash == oldHash)
                return true;

            if (md5Hash == newHash)
            {
                Forms.okayDialog.Display("Already Patched", 260, 40, 30, 16, 10, 
                    "The game is already at v1.1.0 so no patching is needed. Close this patcher and launch the game!");
                return false;
            }
            Forms.okayDialog.Display("Unknown Error", 260, 40, 30, 16, 10, 
                "Something unexpected went wrong. Make sure the game is actually the original v1.0.0 release.");
            return false;
        }
    }
}
