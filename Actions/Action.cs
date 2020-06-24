﻿using System.Collections.Generic;

namespace b64.Actions
{
    abstract class Action
    {
        public Action(IList<string> args)
        {
            Args = args;
        }

        protected virtual string Input { get;  }

        protected IList<string> Args { get; private set; }

        public abstract bool Accept();

        public abstract string Execute();

        public abstract string DescriptionLong { get; }
        public abstract string DescriptionShort { get; }

    }
}
