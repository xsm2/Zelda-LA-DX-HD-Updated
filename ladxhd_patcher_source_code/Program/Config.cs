using System.IO;
using System.Reflection;

namespace LADXHD_Patcher
{
    internal class Config
    {
        // The hash for "newHash" will need to be calculated for each new version.

        public const string version = "1.1.1";
        public const string oldHash = "F4ADFBA864B852908705EA6A18A48F18";
        public const string newHash = "5CCCC37649DDBCA15BE12E6161A641D5";

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
            string md5Hash = Functions.CalculateHash(zeldaEXE, "MD5");
            string Title = "";
            string Message = "";

            if (md5Hash == oldHash)
            {
                return true;
            }
            if (!zeldaEXE.TestPath())
            {
                Title = "Game Executable Not Found";
                Message = "Could not find \"Link's Awakening DX HD.exe\" to patch. Copy this patcher executable to the folder of the original release of v1.0.0 and run it from there.";
                Forms.okayDialog.Display(Title, Message, 250, 40, 27, 10, 15);
                return false;
            }
            if (md5Hash == newHash)
            {
                Title = "Already Patched";
                Message = "The game is already at v1.1.0 so no patching is needed. Close this patcher and launch the game!";
                Forms.okayDialog.Display(Title, Message, 260, 40, 30, 16, 10);
                return false;
            }
            Title = "Unknown Error";
            Message = "Something unexpected went wrong. Make sure the game is actually the original v1.0.0 release.";
            Forms.okayDialog.Display(Title, Message, 260, 40, 30, 16, 10);
            return false;
        }
    }
}
