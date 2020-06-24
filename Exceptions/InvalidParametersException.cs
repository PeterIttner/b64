using System;

namespace b64.Exceptions
{
    class InvalidParametersException : Exception
    {
        public override string Message => "Invalid parameters detected.";
    }
}
