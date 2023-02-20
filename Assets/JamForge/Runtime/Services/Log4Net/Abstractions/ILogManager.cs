using System;
using System.Runtime.CompilerServices;

namespace JamForge.Log4Net
{
    public interface ILogManager
    {
        ILog GetLogger(string name);

        ILog GetLogger(Type type);
    }

    public interface ILogger
    {
        void Debug(object message, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0);

        void Debug(object message, Exception exception, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0);

        void Info(object message, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0);

        void Info(object message, Exception exception, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0);

        void Warn(object message, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0);

        void Warn(object message, Exception exception, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0);

        void Error(object message, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0);

        void Error(object message, Exception exception, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0);

        void Fatal(object message, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0);

        void Fatal(object message, Exception exception, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0);
    }
}
