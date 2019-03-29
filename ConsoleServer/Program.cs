using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;
using Core;

namespace Server
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
            Logger.SetLogger(
                Logger.GetLoggerFactory()
                    .UseNLog("NLog.Config")
                    .Create<Program>()
            );

            if (options.Verbose)
            {
                Logger.SetLevel(LogType.Info);
            }

            Logger.Write(LogType.Info, "Try to start server.");

            //Server.Start(options.Port).Next(result => {
            //    if (result.fail)
            //    {
            //        // @something
            //        // @error message
            //        Logger.WriteLine(LogType.Error, "");
            //        throw new Exception("");
            //    }

            //    Game.Init();
            //    Logger.WriteLine(LogType.Info, $"Run Server in port {options.port}");
            //});
        }

        private static void HandleErrors(IEnumerable<Error> errors)
        {
            Console.WriteLine(errors.ToString());
        }
    }
}
