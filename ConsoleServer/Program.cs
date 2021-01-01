using System;
using System.Collections.Generic;
using CommandLine;
using Core.Config;
using SimpleGame;

namespace ConsoleServer
{
    class ConsoleServerOptions
    {
        [Option('p', "port", Required = true, HelpText = "Input server port.", Default = 9339)]
        public Int32 Port { get; set; }

        [Option('v', null, HelpText = "Print details during execution.")]
        public Boolean Verbose { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<ConsoleServerOptions>(args)
                .WithParsed(RunServer)
                .WithNotParsed(HandleErrors);
        }

        private static void RunServer(ConsoleServerOptions options)
        {
            Console.WriteLine("Try to start server.");

            var game = new Game();
            
            game.Init();

            game.Run();
            
            // TODO: Add program exception handler
        }

        private static void HandleErrors(IEnumerable<Error> errors)
        {
            Console.WriteLine(errors.ToString());
        }

        private class ConsoleServerConfig : Config
        {
            ConsoleServerOptions _opts;

            public ConsoleServerConfig(ConsoleServerOptions opts)
            {
                _opts = opts;
            }

            protected override void Setup()
            {
                Set<Boolean>("LogVerbose", _opts.Verbose);
                Set<Int32>("Port", _opts.Port);
            }
        }
    }
}
