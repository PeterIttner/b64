using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace b64.CommandFramework
{
    class CommandFactory
    {
        public static IList<Command> CreateAll(IList<string> args)
        {
            var types = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Namespace == "b64.Commands" && t.Name.EndsWith("Command") && !t.IsNestedPrivate)
                .ToList();

            var commandlist = new List<Command>();
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type, args) as Command;

                if (instance != null)
                {
                    commandlist.Add(instance);
                }
            }

            return commandlist;
        }
    }
}
