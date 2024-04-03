using b64.CommandFramework;
using System.Collections.Generic;
using System.Diagnostics;

namespace b64.Commands
{
    class UpdateCheckCommand : Command
    {
        public UpdateCheckCommand(IList<string> args) : base(args)
        {

        }

        public override string DescriptionLong => "Check for updates of this tool";
        public override string DescriptionShort => "update check";
        public override string Group => "common";




        public override bool Accept()
        {
            return Args.Count == 2 && Args[0] == "update" && Args[1] == "check";
        }



        public override string Execute()
        {
            var psi = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "tool search b64 --detail",
                RedirectStandardOutput = true,
                UseShellExecute = false
            };
            using (var process = Process.Start(psi))
            {
                process.WaitForExit();

                return process.StandardOutput.ReadToEnd();
            }
        }
    }
}
