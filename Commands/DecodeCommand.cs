using b64.CommandFramework;
using b64.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace b64.Commands
{
    class DecodeCommand : Command
    {
        public DecodeCommand(IList<string> args) : base(args)
        {

        }

        public override string DescriptionLong => "Decodes a base64 ascii text from stdin";
        public override string DescriptionShort => "decode <input>";
        public override string Group => "decode";

        protected override string Input
        {
            get
            {
                return Args[1];
            }
        }

        public override bool Accept()
        {
            return Args.Count == 2 && Args[0] == "decode";
        }

        protected virtual byte[] Decode()
        {
            return Convert.FromBase64String(Input);
        }

        public override string Execute()
        {
            try
            {
                byte[] data = Decode();
                return Encoding.ASCII.GetString(data);
            }
            catch (FormatException)
            {
                throw new InvalidFormatException();
            }
        }
    }
}
