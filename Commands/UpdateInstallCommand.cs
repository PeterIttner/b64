using b64.CommandFramework;
using b64.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace b64.Commands
{
    class UpdateInstallCommand : Command
    {
        public UpdateInstallCommand(IList<string> args) : base(args)
        {

        }

        public override string DescriptionLong => "Install the latest update of this tool";
        public override string DescriptionShort => "update install";
        public override string Group => "common";


    

        public override bool Accept()
        {
            return Args.Count == 2 && Args[0] == "update" && Args[1] == "install";
        }

   

        public override string Execute()
        {
            var psi = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "tool update --global b64",
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
