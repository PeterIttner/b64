using b64.Exceptions;
using System.Collections.Generic;
using System.IO;

namespace b64.Actions
{
    class DecodeFileInputAction : DecodeAction
    {
        public DecodeFileInputAction(IList<string> args) : base(args)
        {
        }

        public override string DescriptionLong => "Decodes a base64 ascii text from the given ascii file";
        public override string DescriptionShort => "decode -f <input-file>";

        public override bool Accept()
        {
            return Args.Count == 3 && Args[0] == "decode" && (Args[1] == "-f" || Args[1] == "<");
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
