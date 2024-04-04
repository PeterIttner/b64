using System;
using System.Collections.Generic;
using System.Linq;

namespace b64.Exceptions
{
    class InvalidParametersException : Exception
    {
        private readonly List<string> _arguments;

        public InvalidParametersException(List<string> arguments)
        {
            _arguments = arguments;
        }
        public override string Message
        {
            get
            {
                if (_arguments == null)
                {
                    return "Invalid parameters detected";
                }
                else
                {
                    return "Invalid parameters detected: " + string.Join(", ", _arguments.Select(x => "{" + x + "}")) + " parameter-count: " + _arguments.Count;
                }
            }
        }
    }
}
