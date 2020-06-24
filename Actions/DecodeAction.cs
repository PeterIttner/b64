using b64.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace b64.Actions
{
    class DecodeAction : Action
    {
        public DecodeAction(IList<string> args) : base(args)
        {

        }

        public override string DescriptionLong => "Decodes a base64 ascii text from stdin";
        public override string DescriptionShort => "decode <input>";

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

        public override string Execute()
        {
            try
            {
                byte[] data = Convert.FromBase64String(Input);
                return Encoding.ASCII.GetString(data);
            }
            catch (FormatException)
            {
                throw new InvalidFormatException();
            }
        }
    }
}
