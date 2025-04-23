using System;

namespace b64.Exceptions
{
    public class InputFileNotFoundException : Exception
    {
        public override string Message => "The input file could not been found.";
    }
}
