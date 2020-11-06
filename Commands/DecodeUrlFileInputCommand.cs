using b64.Exceptions;
using System.Collections.Generic;
using System.IO;

namespace b64.Commands
{
    class DecodeUrlFileInputCommand : DecodeUrlCommand
    {
        public DecodeUrlFileInputCommand(IList<string> args) : base(args)
        {
        }

        public override string DescriptionLong => "Decodes a base64 ascii text from the given ascii file with respecting url encoding";
        public override string DescriptionShort => "decode -[-url|u] -[-file|f] <input-file>";

        public override bool Accept()
        {
            return Args.Count == 4 && Args[0] == "decode" && (Args[1] == "-u" || Args[1] == "--url") && (Args[2] == "--file" || Args[2] == "-f" || Args[2] == "<");
        }

        protected override string Input
        {
            get
            {
                try
                {
                    var inputText = File.ReadAllText(Args[3]);
                    return inputText;
                }
                catch (FileNotFoundException)
                {
                    throw new InputFileNotFoundException();
                }
            }
        }
    }
}
