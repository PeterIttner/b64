using b64.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace b64.Actions
{
    class EncodeAction : Action
    {
        public EncodeAction(IList<string> args) : base(args)
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

        public override string Execute()
        {
            try
            {
                var inputBytes = Encoding.ASCII.GetBytes(Input);
                return Convert.ToBase64String(inputBytes);
            }
            catch (FormatException)
            {
                throw new InvalidFormatException();
            }
        }
    }
}
