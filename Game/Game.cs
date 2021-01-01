using Core;
using Core.Config;
using Core.Network;
using System;
using System.Threading.Tasks;

namespace SimpleGame
{
    public class Game
    {
        Config Config { get; set; }
        Network Network { get; set;}

        public void Init() 
        {
            SetLog();

            Network = new Network();

        }

        public void Run()
        {
            if (Config.Get<Int32>("Port", out var port))
            {
                _ = Network.Start(port).ContinueWith(result =>
                {
                    if (result.Status == TaskStatus.Faulted
                    || result.Status == TaskStatus.Canceled)
                    {
                        Logger.Write(LogType.Error, "Connection Failed.");
                    }
                }).ConfigureAwait(false);
            }
            else
            {
                throw new InLodingGameException(InLodingGameException.EErrorMessage.INVALID_PORT);
            }

            while(true)
            {
                // TODO:
                ;
            }
        }

        void SetLog()
        {
            Logger.SetLogger(
                Logger.GetLoggerFactory()
                    .UseNLog("NLog.Config")
                    .Create<Game>()
            );

            if (Config.Get<Boolean>("LogVerbose", out var value))
            {
                if (value)
                {
                    Logger.SetLevel(LogType.Info);
                }
            }
        }
    }
}
