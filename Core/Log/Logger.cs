using System;

namespace Core
{
    public enum LogType
    {
        Info = 0,
        Warning = 1,
        Error = 2
    }

    public class Logger
    {
        public static Logger _handle = new Logger();
        private Logger() {}
        private LogType _level = LogType.Info;
        
        /* method */

        public void SetLevel(LogType logLevel)
        {
            _level = logLevel;
        }

        public void WriteLine(LogType type, string message)
        {
            var level = (type - _level);
            if(level >= 0)
            {
                Console.WriteLine(message);
            }
        }
    }
}