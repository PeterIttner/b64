using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;

namespace b64.Commands
{
    class EncodeUrlCommand : EncodeCommand
    {
        public EncodeUrlCommand(IList<string> args) : base(args)
        {

        }

        public override string DescriptionLong => "Encodes a base64 ascii text from stdin with respecting url encoding";
        public override string DescriptionShort => "encode -[-url|u] <input>";

        protected override string Input
        {
            get
            {
                return Args[2];
            }
        }

        public override bool Accept()
        {
            return Args.Count == 3 && Args[0] == "encode" && (Args[1] == "--url" || Args[1] == "-u");
        }

        protected override string Encode(byte[] input)
        {
            return WebEncoders.Base64UrlEncode(input);
        }
    }
}
