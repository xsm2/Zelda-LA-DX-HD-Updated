﻿using System.IO;
using System.Reflection;

namespace LADXHD_Patcher
{
    internal class Config
    {
        // The hash for "newHash" will need to be calculated for each new version.
        public const string version = "1.1.4";

        public const string oldHash = "F4ADFBA864B852908705EA6A18A48F18";
        public const string newHash = "6C14C1EF334477A5FAED010513B37ED5";

        public static string appPath;
        public static string baseFolder;
        public static string tempFolder;
        public static string zeldaEXE;
        public static string backupPath;

        public static void Initialize()
        {
            appPath = Assembly.GetExecutingAssembly().Location;
            baseFolder = Path.GetDirectoryName(appPath);
            tempFolder = Path.Combine(baseFolder, "~temp");
            zeldaEXE = Path.Combine(baseFolder, "Link's Awakening DX HD.exe");
            backupPath = (Path.Combine(baseFolder, "Data", "Backup")).CreatePath();
        }
    }
}
