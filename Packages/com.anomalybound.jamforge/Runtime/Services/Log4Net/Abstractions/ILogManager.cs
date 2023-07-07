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
        void D(object message, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0);

        void D(object message, Exception exception, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0);

        void I(object message, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0);

        void I(object message, Exception exception, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0);

        void W(object message, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0);

        void W(object message, Exception exception, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0);

        void E(object message, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0);

        void E(object message, Exception exception, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0);

        void F(object message, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0);

        void F(object message, Exception exception, [CallerMemberName] string callerName = null, [CallerLineNumber] int callerLine = 0);
    }
}
