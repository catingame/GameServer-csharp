using System;
using System.Threading;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Core
{
    public enum LogType
    {
        Trace = LogLevel.Trace,
        Debug = LogLevel.Debug,
        Info = LogLevel.Information,
        Warning = LogLevel.Warning,
        Error = LogLevel.Error,
        Fatal = LogLevel.Critical,
        None = LogLevel.None
    }

    public static class LoggerExtension
    {
        public static ILoggerFactory UseNLog(this ILoggerFactory factory, String configFile)
        {
            NLog.LogManager.LoadConfiguration(configFile);
            return factory.AddNLog();
        }
        public static ILogger Create<T>(this ILoggerFactory factory)
        {
            return factory.CreateLogger<T>();
        }
    }

    public class Logger
    {
        private static ILogger _logger;
        private static readonly ILoggerFactory _factory = new LoggerFactory();
        private static LogLevel _curLogLevel;

        public static ILoggerFactory GetLoggerFactory()
        {
            return _factory;
        }

        public static ILogger Create<T>()
        {
            return _factory.CreateLogger<T>();
        }

        public static void SetLogger(ILogger logger)
        {
            Interlocked.Exchange(ref _logger, logger);
        }

        public static void SetLevel(LogType type)
        {
            _curLogLevel = (LogLevel)type;
        }

        /* method */

        public static void Write(LogType type, String message, params Object[] args)
        {
            var originLogLevelType = (LogLevel)type;
            var logLevel = (originLogLevelType - _curLogLevel);

            if(_logger != null && logLevel >= 0)
            {
                _logger.Log(originLogLevelType, message, args);
            }
        }

        // TODO: need to implement more variety method like Exception, eventId
    }
}