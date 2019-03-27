using System;
using Logger;
using CommandLine;

namespace Server
{
    class Options
    {
        [Option("p", "port", Required = true, HelpText = "Input server port.", Default = 9339)]
        public Int32 port { get; set; }

        [Option("v", null, HelpText = "Print details during execution.")]
        public Boolean Verbose { get; set; }

        [HelpOption(HelpText = "Display this help screen.")]
        public String GetUsage()
        {
            var usage = new StringBuilder();
            usage.AppendLine("ConsoleServer Application 1.0");
            usage.AppendLine("Read user manual for usage instructions...");
            return usage.ToString();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                if(options.Verbose)
                {
                    Logger.SetLevel(LogType.Info);
                }

                Logger.WriteLine(LogType.Info, "Try to start server.");

                Server.Start(options.port).Next(result => {
                    if(result.fail) 
                    {
                        // @something
                        // @error message
                        Logger.WriteLine(LogType.Error, "");
                        throw new Exception("");
                    }

                    Game.Init();
                    Logger.WriteLine(LogType.Info, $"Run Server in port {options.port}");
                });
            }
            else
            {
                Console.WriteLine(options.GetUsage());
            }
        }
    }
}
