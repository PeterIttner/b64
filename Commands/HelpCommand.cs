using b64.CommandFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace b64.Commands
{
    class HelpCommand : Command
    {
        public HelpCommand(IList<string> args) : base(args)
        {

        }

        public override string DescriptionLong => "Displays this help message";
        public override string DescriptionShort => "--help|-help|help|-h|/?";
        public override string Group => "common";

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

            var groups = CommandFactory
                .CreateAll(null)
                .GroupBy(x => x.Group)
                .OrderBy(x => x.Key);

            var descriptions = groups
                .Select(group => FormatGroupHeadline(group.Key) +
                    string.Join(Environment.NewLine, 
                    
                        group
                        .OrderBy(x => x.DescriptionShort)
                        .Select(x => FormatAction(x.DescriptionShort, x.DescriptionLong))
                    )
                ).ToList();
            
            var text = string.Join(Environment.NewLine, descriptions);


            return @$"{executableName} <actions>

{AsciiArt()}

Description:
  Encodes or decodes base64 ascii texts.

Actions:
{text}";
        }

        private static string FormatGroupHeadline(string group)
        {
            var text = group + " actions";
            var seperator = new string('*', text.Length);
            return Environment.NewLine + "".PadRight(41) + seperator + Environment.NewLine + "".PadRight(41) + text + Environment.NewLine + "".PadRight(41) + seperator + Environment.NewLine + Environment.NewLine;
        }

        private static string FormatAction(string descriptionShort, string descriptionLong)
        {
            return descriptionShort.PadRight(41) + ": " + descriptionLong;
        }

        private static string AsciiArt()
        {
            return """
             _      __   _  _   
            | |__  / /_ | || |  
            | '_ \| '_ \| || |_ 
            | |_) | (_) |__   _|
            |_.__/ \___/   |_|  
                                
            """;
        }
    }
}
