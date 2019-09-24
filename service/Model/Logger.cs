using System;

namespace OrienteeringTvResults.Model
{
    public static class Logger
    {
        private static log4net.ILog log;

        public static void Initialize()
        {
            var logRepository = log4net.LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
            var fileInfo = new System.IO.FileInfo("log4net.config");
            log4net.Config.XmlConfigurator.Configure(logRepository, fileInfo);
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public static void LogInfo(object message)
        {
            log.Info(message);
        }

        public static void LogWarn(object message)
        {
            log.Warn(message);
        }

        public static void LogError(object message, Exception exception = null)
        {
            log.Fatal(message, exception);
        }

        public static void LogFatal(object message, Exception exception)
        {
            log.Fatal(message, exception);
        }
    }
}