using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using log4net;
using UnityEngine;

namespace JamForge.Log4Net
{
    public static class JFLog
    {
        private const string Log4NetConfig = "Log4NetConfig";
        
        static JFLog()
        {
            InitializeLog4NetConfig();
        }

        private static void InitializeLog4NetConfig()
        {
            var configText = Resources.Load<TextAsset>(Log4NetConfig);
            if (configText == null) { return; }

            using var memStream = new MemoryStream(configText.bytes);
            log4net.Config.XmlConfigurator.Configure(memStream);
        }
        
        public static ILog GetLogger(Type type) => new JamForgeLogger(LogManager.GetLogger(type));

        public static ILog GetLogger<T>() => new JamForgeLogger(LogManager.GetLogger(typeof(T)));

        public static ILog GetLogger(string name) => new JamForgeLogger(LogManager.GetLogger(name));

        private static readonly Dictionary<(string, int), ILog> LOGCaches = new();

        [HideInCallstack]
        private static ILog GetLogger(string callerMemberName, int callerLineNumber)
        {
            var key = (callerMemberName, callerLineNumber);
            if (LOGCaches.TryGetValue(key, out var log)) { return log; }

            var stackTrace = new StackTrace();
            var callerType = stackTrace.GetFrame(2).GetMethod().DeclaringType;
            var callerTypeName = $"{callerType?.Name}/{callerMemberName}";

            log = GetLogger(callerTypeName);
            LOGCaches[key] = log;
            return log;
        }

        public static void Debug(object message, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0)
        {
            GetLogger(callerName, callerLine).Debug(message);
        }

        public static void Debug(object message, Exception exception, [CallerMemberName] string callerName = null,
            [CallerLineNumber] int callerLine = 0)
        {
            GetLogger(callerName, callerLine).Debug(message, exception);
        }

        public static void Info(object message, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0)
        {
            GetLogger(callerName, callerLine).Info(message);
        }

        public static void Info(object message, Exception exception, [CallerMemberName] string callerName = null,
            [CallerLineNumber] int callerLine = 0)
        {
            GetLogger(callerName, callerLine).Info(message, exception);
        }

        public static void Warn(object message, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0)
        {
            GetLogger(callerName, callerLine).Warn(message);
        }

        public static void Warn(object message, Exception exception, [CallerMemberName] string callerName = null,
            [CallerLineNumber] int callerLine = 0)
        {
            GetLogger(callerName, callerLine).Warn(message, exception);
        }

        public static void Error(object message, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0)
        {
            GetLogger(callerName, callerLine).Error(message);
        }

        public static void Error(object message, Exception exception, [CallerMemberName] string callerName = null,
            [CallerLineNumber] int callerLine = 0)
        {
            GetLogger(callerName, callerLine).Error(message, exception);
        }

        public static void Fatal(object message, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0)
        {
            GetLogger(callerName, callerLine).Fatal(message);
        }

        public static void Fatal(object message, Exception exception, [CallerMemberName] string callerName = null,
            [CallerLineNumber] int callerLine = 0)
        {
            GetLogger(callerName, callerLine).Fatal(message, exception);
        }
    }
}
