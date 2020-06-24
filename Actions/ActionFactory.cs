using System;
using System.Collections.Generic;

namespace b64.Actions
{
    class ActionFactory
    {
        public static IList<Action> CreateAll(IList<string> args)
        {
            return new List<Action>
            {
                new HelpAction(args),
                new VersionAction(args),
                new DecodeAction(args),
                new DecodeFileInputAction(args),
                new EncodeAction(args),
                new EncodeFileInputAction(args)
            };
        }
    }
}
