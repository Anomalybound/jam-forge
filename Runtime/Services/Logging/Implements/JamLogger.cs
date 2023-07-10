using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace JamForge.Logging
{
    [Preserve]
    public class JamLogger : ILogger
    {
        public event LogDelegate LogDelegate;

        public string Name { get; internal set; }

        public JamLogLevel LogLevel { get; internal set; }

        public JamLogger()
        {
            LogLevel = JamLogLevel.Debug;
        }

        [HideInCallstack]
        public void T(string message) => Log(JamLogLevel.Trace, message);

        [HideInCallstack]
        public void D(string message) => Log(JamLogLevel.Debug, message);

        [HideInCallstack]
        public void I(string message) => Log(JamLogLevel.Info, message);

        [HideInCallstack]
        public void W(string message) => Log(JamLogLevel.Warn, message);

        [HideInCallstack]
        public void E(string message) => Log(JamLogLevel.Error, message);

        [HideInCallstack]
        public void F(string message) => Log(JamLogLevel.Fatal, message);

        public void Assert(bool condition, string message)
        {
            if (!condition)
            {
                throw new JamAssertException(message);
            }
        }

#if JAMLOG_OFF
        [System.Diagnostics.Conditional("false")]
#endif
        [HideInCallstack]
        internal void Log(JamLogLevel logLvl, string message)
        {
            if (logLvl >= LogLevel)
            {
                LogDelegate?.Invoke(this, logLvl, message);
            }
        }

        public void Reset() => LogDelegate = null;

        internal void SetAppender(LogDelegate logDelegate)
        {
            LogDelegate = logDelegate;
        }
    }
}

public class JamAssertException : Exception
{
    public JamAssertException(string message) : base(message) { }
}
