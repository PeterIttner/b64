using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace b64.Commands
{
    class DecodeUrlCommand : DecodeCommand
    {
        public DecodeUrlCommand(IList<string> args) : base(args)
        {

        }

        public override string DescriptionLong => "Decodes a base64 ascii text from stdin with respecting url encoding";
        public override string DescriptionShort => "decode -[-url|u] <input>";

        protected override string Input
        {
            get
            {
                return Args[2];
            }
        }

        public override bool Accept()
        {
            return Args.Count == 3 && Args[0] == "decode" && (Args[1] == "-u" || Args[1] == "--url");
        }

        protected override string Decode()
        {
            return Base64UrlEncoder.Decode(Input);
        }
    }
}
