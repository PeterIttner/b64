using b64.CommandFramework;
using b64.Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace b64.Commands
{
    class VersionCommand : Command
    {
        public VersionCommand(IList<string> args) : base(args)
        {

        }

        public override string DescriptionLong => "Displays the current version of the executable";
        public override string DescriptionShort => "-v|version";

       
        public override bool Accept()
        {
            return Args.Count == 1 && (Args[0] == "-v" || Args[0] == "version");
        }

        public override string Execute()
        {
            return typeof(Program).Assembly.GetName().Version.ToString();
        }
    }
}
