using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;
using UnityEngine;
using VContainer;
using ILogger = log4net.Core.ILogger;

namespace JamForge
{
    public interface ILog : log4net.ILog { }

    public readonly struct JamForgeLogger : ILog
    {
        private readonly log4net.ILog _logImpl;

        public JamForgeLogger(log4net.ILog logImpl)
        {
            _logImpl = logImpl;
        }

        ILogger ILoggerWrapper.Logger => _logImpl.Logger;

        [HideInCallstack]
        public void Debug(object message)
        {
            _logImpl.Debug(message);
        }

        [HideInCallstack]
        public void Debug(object message, Exception exception)
        {
            _logImpl.Debug(message, exception);
        }

        [HideInCallstack]
        public void DebugFormat(string format, params object[] args)
        {
            _logImpl.DebugFormat(format, args);
        }

        [HideInCallstack]
        public void DebugFormat(string format, object arg0)
        {
            _logImpl.DebugFormat(format, arg0);
        }

        [HideInCallstack]
        public void DebugFormat(string format, object arg0, object arg1)
        {
            _logImpl.DebugFormat(format, arg0, arg1);
        }

        [HideInCallstack]
        public void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            _logImpl.DebugFormat(format, arg0, arg1, arg2);
        }

        [HideInCallstack]
        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            _logImpl.DebugFormat(provider, format, args);
        }

        [HideInCallstack]
        public void Info(object message)
        {
            _logImpl.Info(message);
        }

        [HideInCallstack]
        public void Info(object message, Exception exception)
        {
            _logImpl.Info(message, exception);
        }

        [HideInCallstack]
        public void InfoFormat(string format, params object[] args)
        {
            _logImpl.InfoFormat(format, args);
        }

        [HideInCallstack]
        public void InfoFormat(string format, object arg0)
        {
            _logImpl.InfoFormat(format, arg0);
        }

        [HideInCallstack]
        public void InfoFormat(string format, object arg0, object arg1)
        {
            _logImpl.InfoFormat(format, arg0, arg1);
        }

        [HideInCallstack]
        public void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            _logImpl.InfoFormat(format, arg0, arg1, arg2);
        }

        [HideInCallstack]
        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            _logImpl.InfoFormat(provider, format, args);
        }

        [HideInCallstack]
        public void Warn(object message)
        {
            _logImpl.Warn(message);
        }

        [HideInCallstack]
        public void Warn(object message, Exception exception)
        {
            _logImpl.Warn(message, exception);
        }

        [HideInCallstack]
        public void WarnFormat(string format, params object[] args)
        {
            _logImpl.WarnFormat(format, args);
        }

        [HideInCallstack]
        public void WarnFormat(string format, object arg0)
        {
            _logImpl.WarnFormat(format, arg0);
        }

        [HideInCallstack]
        public void WarnFormat(string format, object arg0, object arg1)
        {
            _logImpl.WarnFormat(format, arg0, arg1);
        }

        [HideInCallstack]
        public void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            _logImpl.WarnFormat(format, arg0, arg1, arg2);
        }

        [HideInCallstack]
        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            _logImpl.WarnFormat(provider, format, args);
        }

        [HideInCallstack]
        public void Error(object message)
        {
            _logImpl.Error(message);
        }

        [HideInCallstack]
        public void Error(object message, Exception exception)
        {
            _logImpl.Error(message, exception);
        }

        [HideInCallstack]
        public void ErrorFormat(string format, params object[] args)
        {
            _logImpl.ErrorFormat(format, args);
        }

        [HideInCallstack]
        public void ErrorFormat(string format, object arg0)
        {
            _logImpl.ErrorFormat(format, arg0);
        }

        [HideInCallstack]
        public void ErrorFormat(string format, object arg0, object arg1)
        {
            _logImpl.ErrorFormat(format, arg0, arg1);
        }

        [HideInCallstack]
        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            _logImpl.ErrorFormat(format, arg0, arg1, arg2);
        }

        [HideInCallstack]
        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            _logImpl.ErrorFormat(provider, format, args);
        }

        [HideInCallstack]
        public void Fatal(object message)
        {
            _logImpl.Fatal(message);
        }

        [HideInCallstack]
        public void Fatal(object message, Exception exception)
        {
            _logImpl.Fatal(message, exception);
        }

        [HideInCallstack]
        public void FatalFormat(string format, params object[] args)
        {
            _logImpl.FatalFormat(format, args);
        }

        [HideInCallstack]
        public void FatalFormat(string format, object arg0)
        {
            _logImpl.FatalFormat(format, arg0);
        }

        [HideInCallstack]
        public void FatalFormat(string format, object arg0, object arg1)
        {
            _logImpl.FatalFormat(format, arg0, arg1);
        }

        [HideInCallstack]
        public void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            _logImpl.FatalFormat(format, arg0, arg1, arg2);
        }

        [HideInCallstack]
        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            _logImpl.FatalFormat(provider, format, args);
        }

        public bool IsDebugEnabled => _logImpl.IsDebugEnabled;

        public bool IsInfoEnabled => _logImpl.IsInfoEnabled;

        public bool IsWarnEnabled => _logImpl.IsWarnEnabled;

        public bool IsErrorEnabled => _logImpl.IsErrorEnabled;

        public bool IsFatalEnabled => _logImpl.IsFatalEnabled;
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class Log4NetWrapper
    {
        private const string Log4NetConfig = "Log4NetConfig";

        public static Log4NetWrapper Current { get; } = CreateInstance();

        private Log4NetWrapper()
        {
            InitializeLog4NetConfig();
        }

        private static Log4NetWrapper CreateInstance()
        {
            return new Log4NetWrapper();
        }

        private static void InitializeLog4NetConfig()
        {
            var configText = Resources.Load<TextAsset>(Log4NetConfig);
            if (configText == null) { return; }

            using var memStream = new MemoryStream(configText.bytes);
            log4net.Config.XmlConfigurator.Configure(memStream);
        }

        public ILog GetLog(string name)
        {
            return new JamForgeLogger(log4net.LogManager.GetLogger(name));
        }

        public ILog GetLog(Type type)
        {
            return new JamForgeLogger(log4net.LogManager.GetLogger(type));
        }

        private readonly Dictionary<(string, int), ILog> _logCaches = new();

        [HideInCallstack]
        private ILog GetLog(string callerMemberName, int callerLineNumber)
        {
            var key = (callerMemberName, callerLineNumber);
            if (_logCaches.TryGetValue(key, out var log)) { return log; }

            var stackTrace = new StackTrace();
            var callerType = stackTrace.GetFrame(2).GetMethod().DeclaringType;
            var callerTypeName = $"{callerType?.Name}/{callerMemberName}";

            log = GetLog(callerTypeName);
            _logCaches[key] = log;
            return log;
        }

        #region implementation of ILog

        [HideInCallstack]
        public void Debug(object message, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            GetLog(callerMemberName, callerLineNumber).Debug(message);
        }

        public void Debug(object message, Exception exception, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            GetLog(callerMemberName, callerLineNumber).Debug(message, exception);
        }

        public void Info(object message, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            GetLog(callerMemberName, callerLineNumber).Info(message);
        }

        public void Info(object message, Exception exception, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            GetLog(callerMemberName, callerLineNumber).Info(message, exception);
        }

        public void Warn(object message, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            GetLog(callerMemberName, callerLineNumber).Warn(message);
        }

        public void Warn(object message, Exception exception, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            GetLog(callerMemberName, callerLineNumber).Warn(message, exception);
        }

        public void Error(object message, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            GetLog(callerMemberName, callerLineNumber).Error(message);
        }

        public void Error(object message, Exception exception, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            GetLog(callerMemberName, callerLineNumber).Error(message, exception);
        }

        public void Fatal(object message, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            GetLog(callerMemberName, callerLineNumber).Fatal(message);
        }

        public void Fatal(object message, Exception exception, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            GetLog(callerMemberName, callerLineNumber).Fatal(message, exception);
        }

        #endregion
    }

    public partial class Jam
    {
        #region Logs

        [Inject]
        private Log4NetWrapper _logger;

        public static Log4NetWrapper Logger => Instance._logger;

        #endregion
    }
}
