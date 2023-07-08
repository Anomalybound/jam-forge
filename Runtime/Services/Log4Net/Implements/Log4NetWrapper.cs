using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace JamForge.Log4Net
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class Log4NetWrapper : ILogWrapper
    {
        public ILog GetLogger(string name) => JFLog.GetLogger(name);

        public ILog GetLogger(Type type) => JFLog.GetLogger(type);

        #region implementation of ILog

        [HideInCallstack]
        public void D(object message, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0) => JFLog.Debug(message, callerMemberName, callerLineNumber);

        public void D(object message, Exception exception, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0) => JFLog.Debug(message, exception, callerMemberName, callerLineNumber);

        public void I(object message, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0) => JFLog.Info(message, callerMemberName, callerLineNumber);

        public void I(object message, Exception exception, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0) => JFLog.Info(message, exception, callerMemberName, callerLineNumber);

        public void W(object message, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0) => JFLog.Warn(message, callerMemberName, callerLineNumber);

        public void W(object message, Exception exception, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0) => JFLog.Warn(message, exception, callerMemberName, callerLineNumber);

        public void E(object message, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0) => JFLog.Error(message, callerMemberName, callerLineNumber);

        public void E(object message, Exception exception, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0) => JFLog.Error(message, exception, callerMemberName, callerLineNumber);

        public void F(object message, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0) => JFLog.Fatal(message, callerMemberName, callerLineNumber);

        public void F(object message, Exception exception, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0) => JFLog.Fatal(message, exception, callerMemberName, callerLineNumber);

        #endregion
    }
}
