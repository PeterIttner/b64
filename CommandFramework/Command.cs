using System.Collections.Generic;

namespace b64.CommandFramework
{
    abstract class Command
    {
        public Command(IList<string> args)
        {
            Args = args;
        }

        protected virtual string Input { get; }

        protected IList<string> Args { get; private set; }

        public abstract bool Accept();

        public abstract string Execute();

        public abstract string DescriptionLong { get; }
        public abstract string DescriptionShort { get; }
        public abstract string Group { get; }
    }
}
