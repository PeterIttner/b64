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
        public override string Group => "encode";


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

        protected virtual string Encode(string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes);
        }

        public override string Execute()
        {
            try
            {
                return Encode(Input);
            }
            catch (FormatException)
            {
                throw new InvalidFormatException();
            }
        }
    }
}
