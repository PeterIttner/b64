using b64.Exceptions;
using System.Collections.Generic;
using System.IO;

namespace b64.Commands
{
    class EncodeFileInputCommand : EncodeCommand
    {
        public EncodeFileInputCommand(IList<string> args) : base(args)
        {
        }

        public override string DescriptionLong => "Encodes a base64 ascii text from the given ascii file";
        public override string DescriptionShort => "encode -[-file|f] <input-file>";
        public override bool Accept()
        {
            return Args.Count == 3 && Args[0] == "encode" && (Args[1] == "--file" || Args[1] == "-f" || Args[1] == "<");
        }

        protected override string Input
        {
            get
            {
                try
                {
                    var inputText = File.ReadAllText(Args[2]);
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
