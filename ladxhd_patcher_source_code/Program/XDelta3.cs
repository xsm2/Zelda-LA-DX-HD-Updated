using System.Diagnostics;

namespace LADXHD_Patcher
{
    internal class XDelta3
    {
        public static string Exe;
        public static string Args;

        public static void Initialize()
        {
            XDelta3.Exe = Config.tempFolder + "\\xdelta3.exe";
        }

        public static string GetCreateArguments(string OldFile, string NewFile, string PatchFile)
        {
		    string args = string.Empty;
		    args = string.Concat(new string[]
		    {
			    args,
			    " -f -s \"",
			    OldFile,
			    "\" \"",
			    NewFile,
			    "\" \"",
			    PatchFile,
			    "\""
		    });
            return args;
        }

        public static string GetApplyArguments(string OldFile, string PatchFile, string NewFile)
        {
		    string args = string.Empty;
		    args = string.Concat(new string[]
		    {
			    args,
			    " -d -f -s \"",
			    OldFile,
			    "\" \"",
			    PatchFile,
			    "\" \"",
			    NewFile,
			    "\""
		    });
            return args;
        }

        public static void Start()
        {
            Process xDelta = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo 
            {
                WorkingDirectory = Config.baseFolder,
                FileName = XDelta3.Exe,
                Arguments = XDelta3.Args,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                WindowStyle = ProcessWindowStyle.Normal
            };
            xDelta.StartInfo = startInfo;
            xDelta.Start();
            xDelta.WaitForExit();
        }
    }
}
