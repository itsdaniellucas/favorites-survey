using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using NLogLogger = NLog.Logger;

namespace FavoritesSurvey.Core
{
    public static class Logger
    {
        private static NLogLogger _logger = LogManager.GetCurrentClassLogger();

        private static void BaseLog(LogLevel level, object obj, LogVariables logVar = null)
        {
            if (logVar == null)
                logVar = new LogVariables();

            LogEventInfo logEvent = new LogEventInfo(level, "", obj.ToString());
            logEvent.Properties["CorrelationKey"] = logVar.CorrelationKey;
            logEvent.Properties["RequestPath"] = logVar.RequestPath;
            logEvent.Properties["HttpVerb"] = logVar.HttpVerb;
            logEvent.Properties["CurrentUser"] = logVar.CurrentUser;
            logEvent.Properties["Environment"] = logVar.Environment;
            _logger.Log(logEvent);
        }

        public static void Fatal(object obj, LogVariables logVar = null)
        {
            BaseLog(LogLevel.Fatal, obj, logVar);
        }

        public static void Error(object obj, LogVariables logVar = null)
        {
            BaseLog(LogLevel.Error, obj, logVar);
        }

        public static void Warn(object obj, LogVariables logVar = null)
        {
            BaseLog(LogLevel.Warn, obj, logVar);
        }

        public static void Info(object obj, LogVariables logVar = null)
        {
            BaseLog(LogLevel.Info, obj, logVar);
        }

        public static void Debug(object obj, LogVariables logVar = null)
        {
            BaseLog(LogLevel.Debug, obj, logVar);
        }

        public static void Trace(object obj, LogVariables logVar = null)
        {
            BaseLog(LogLevel.Trace, obj, logVar);
        }

    }
}
