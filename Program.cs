using b64.Actions;
using b64.Exceptions;
using System;
using System.IO;
using System.Linq;

namespace b64
{
    class Program
    {
        static void Main(string[] args)
        {
            var arguments = args.ToList();

            if (Console.IsInputRedirected)
            {
                using (Stream pipestream = Console.OpenStandardInput())
                {
                    var reader = new StreamReader(pipestream);
                    arguments.Add(reader.ReadToEnd());
                }
            }

            try
            {
                var action = ActionFactory.CreateAll(arguments).FirstOrDefault(a => a.Accept());
                if (action != null)
                {
                    Console.WriteLine(action.Execute());
                }
                else
                {
                    throw new InvalidParametersException();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Environment.Exit(1);
            }
        }
    }
}
