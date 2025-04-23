using System;

namespace b64.Exceptions
{
    public class InvalidFormatException : Exception
    {
        public override string Message => "Invalid format detected.";
    }
}
