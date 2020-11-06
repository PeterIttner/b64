using b64.CommandFramework;
using b64.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace b64.Commands
{
    class EncodeCommand : Command
    {
        public EncodeCommand(IList<string> args) : base(args)
        {

        }

        public override string DescriptionLong => "Encodes a base64 ascii text from stdin";
        public override string DescriptionShort => "encode <input>";

        protected override string Input
        {
            get
            {
                return Args[1];
            }
        }

        public override bool Accept()
        {
            return Args.Count == 2 && Args[0] == "encode";
        }

        protected virtual string Encode(byte[] input)
        {
            return Convert.ToBase64String(input);
        }

        public override string Execute()
        {
            try
            {
                var inputBytes = Encoding.ASCII.GetBytes(Input);
                return Encode(inputBytes);
            }
            catch (FormatException)
            {
                throw new InvalidFormatException();
            }
        }
    }
}
