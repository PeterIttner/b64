using System;

namespace b64.Exceptions
{
    class InvalidFormatException : Exception
    {
        public override string Message => "Invalid format detected.";
    }
}
