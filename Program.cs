using b64.CommandFramework;
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
            var arguments = args
                .Where(arg => !string.IsNullOrWhiteSpace(arg))
                .Where(arg => !string.IsNullOrEmpty(arg))
                .ToList();

            if (Console.IsInputRedirected)
            {
                using (Stream pipestream = Console.OpenStandardInput())
                {
                    var reader = new StreamReader(pipestream);
                    var arg = reader.ReadToEnd();
                    if(!string.IsNullOrEmpty(arg) && !string.IsNullOrWhiteSpace(arg))
                    {
                        arguments.Add(arg);
                    }
                }
            }

            try
            {
                var action = CommandFactory.CreateAll(arguments).FirstOrDefault(a => a.Accept());
                if (action != null)
                {
                    Console.Write(action.Execute());
                }
                else
                {
                    throw new InvalidParametersException(arguments);
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
