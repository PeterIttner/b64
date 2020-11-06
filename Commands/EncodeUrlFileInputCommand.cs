using b64.Exceptions;
using System.Collections.Generic;
using System.IO;

namespace b64.Commands
{
    class EncodeUrlFileInputCommand : EncodeUrlCommand
    {
        public EncodeUrlFileInputCommand(IList<string> args) : base(args)
        {
        }

        public override string DescriptionLong => "Encodes a base64 ascii text from the given ascii file with respecting url encoding";
        public override string DescriptionShort => "encode -[-url|u] -[-file|f] <input-file>";
        public override bool Accept()
        {
            return Args.Count == 4 && Args[0] == "encode" && (Args[1] == "--url" || Args[1] == "-u") && (Args[2] == "--file" || Args[2] == "-f" || Args[2] == "<");
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
