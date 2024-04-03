using b64.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using b64.CommandFramework;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace b64.Commands
{
    class DecodeJWTCommand : Command
    {
        public DecodeJWTCommand(IList<string> args) : base(args)
        {

        }

        public override string DescriptionLong => "Decodes a jwt ascii text from stdin";
        public override string DescriptionShort => "decode --jwt <input>";
        public override string Group => "decode";

        protected override string Input
        {
            get
            {
                return Args[2];
            }
        }

        public override bool Accept()
        {
            return Args.Count == 3 && Args[0] == "decode" && Args[1] == "--jwt";
        }

        private string DecodeAsJson(string input)
        {
            var data = WebEncoders.Base64UrlDecode(input);
            var raw = Encoding.ASCII.GetString(data);
            return FormatJson(raw);
        }

        private static string FormatJson(string json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }

        public override string Execute()
        {
            try
            {
                var parts = Input.Split(".");
                if(parts.Length != 3)
                {
                    throw new InvalidFormatException();
                }
                var header = DecodeAsJson(parts[0]);
                var payload = DecodeAsJson(parts[1]);

                return @$"JWT:

Header
======
{header}

Payload
=======
{payload}";
            }
            catch (FormatException)
            {
                throw new InvalidFormatException();
            }
        }
    }
}
