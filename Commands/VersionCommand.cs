using b64.CommandFramework;
using System.Collections.Generic;

namespace b64.Commands
{
    class VersionCommand : Command
    {
        public VersionCommand(IList<string> args) : base(args)
        {

        }

        public override string DescriptionLong => "Displays the current version of the executable";
        public override string DescriptionShort => "-v|version";
        public override string Group => "common";

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
