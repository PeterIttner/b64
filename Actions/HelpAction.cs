using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace b64.Actions
{
    class HelpAction : Action
    {
        public HelpAction(IList<string> args) : base(args)
        {

        }

        public override string DescriptionLong => "Displays this help message";
        public override string DescriptionShort => "--help|-help|help|-h|/?";

        public override bool Accept()
        {
            return Args.Count == 0 ||
                Args.Count >= 1 && (Args[0] == "-h" || Args[0] == "help" || Args[0] == "-help" || Args[0] == "--help" || Args[0] == "/?");
        }

        public override string Execute()
        {
            var executableName = AppDomain.CurrentDomain.FriendlyName;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                executableName += ".exe";
            }

            var descriptions = ActionFactory.CreateAll(null).Select(a => a.DescriptionShort.PadRight(24) + ": " + a.DescriptionLong).ToList();
            var text = string.Join(Environment.NewLine + "  - ", descriptions);
            return @$"{executableName} <actions>

Description:
  Encodes or decodes base64 ascii texts.

Actions:
  - {text}";
        }
    }
}
