using log4net;
using log4net.Config;
using System.Collections.Generic;
using log4net.Repository;
using System;
using System.IO;
using System.Reflection;

namespace VoxelValley.Common.Diagnostics
{
  public static class Log
    {
        private static readonly Dictionary<Type, ILog> loggers = new Dictionary<Type, ILog>();
        private static readonly object _lock = new object();

        static Log()
        {
            ILoggerRepository logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("Common/Assets/Configuration/log4net.config"));
        }

        private static ILog GetLogger(Type source)
        {
            lock (_lock)
            {
                if (loggers.ContainsKey(source))
                    return loggers[source];
                else
                {
                    ILog logger = LogManager.GetLogger(source);
                    loggers.Add(source, logger);
                    return logger;
                }
            }
        }

        public static void Debug(Type source, object message, Exception ex = null)
        {
            ILog logger = GetLogger(source);
            if (logger.IsDebugEnabled)
                logger.Debug(message, ex);
        }

        public static void Info(Type source, object message, Exception ex = null)
        {
            ILog logger = GetLogger(source);
            if (logger.IsInfoEnabled)
                logger.Info(message, ex);
        }

        public static void Warn(Type source, object message, Exception ex = null)
        {
            ILog logger = GetLogger(source);
            if (logger.IsWarnEnabled)
                logger.Warn(message, ex);
        }

        public static void Error(Type source, object message, Exception ex = null)
        {
            ILog logger = GetLogger(source);
            if (logger.IsErrorEnabled)
                logger.Error(message, ex);
        }

        public static void Fatal(Type source, object message, Exception ex = null)
        {
            ILog logger = GetLogger(source);
            if (logger.IsFatalEnabled)
                logger.Fatal(message, ex);
        }
    }
}