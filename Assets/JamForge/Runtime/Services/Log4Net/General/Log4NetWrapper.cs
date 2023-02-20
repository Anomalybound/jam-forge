using System;
using System.Runtime.CompilerServices;
using JamForge.Log4Net;
using UnityEngine;

namespace JamForge
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class Log4NetWrapper : ILogWrapper
    {
        public ILog GetLogger(string name) => JFLog.GetLogger(name);

        public ILog GetLogger(Type type) => JFLog.GetLogger(type);

        #region implementation of ILog

        [HideInCallstack]
        public void Debug(object message, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0) => JFLog.Debug(message, callerMemberName, callerLineNumber);

        public void Debug(object message, Exception exception, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0) => JFLog.Debug(message, exception, callerMemberName, callerLineNumber);

        public void Info(object message, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0) => JFLog.Info(message, callerMemberName, callerLineNumber);

        public void Info(object message, Exception exception, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0) => JFLog.Info(message, exception, callerMemberName, callerLineNumber);

        public void Warn(object message, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0) => JFLog.Warn(message, callerMemberName, callerLineNumber);

        public void Warn(object message, Exception exception, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0) => JFLog.Warn(message, exception, callerMemberName, callerLineNumber);

        public void Error(object message, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0) => JFLog.Error(message, callerMemberName, callerLineNumber);

        public void Error(object message, Exception exception, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0) => JFLog.Error(message, exception, callerMemberName, callerLineNumber);

        public void Fatal(object message, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0) => JFLog.Fatal(message, callerMemberName, callerLineNumber);

        public void Fatal(object message, Exception exception, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0) => JFLog.Fatal(message, exception, callerMemberName, callerLineNumber);

        #endregion
    }
}
