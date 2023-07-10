using System;
using System.Collections.Generic;
using UnityEngine;

namespace JamForge.Logging
{
    public class UnityLogAppender : FormattedAppender
    {
        public UnityLogAppender(List<ILogFormatter> formatters) : base(formatters) { }

        [HideInCallstack]
        protected override void WriteFormattedLine(JamLogger logger, JamLogLevel logLevel, string message)
        {
            switch (logLevel)
            {
                case JamLogLevel.Trace:
                case JamLogLevel.Debug:
                case JamLogLevel.Info:
                    Debug.Log(message);
                    return;
                case JamLogLevel.Warn:
                    Debug.LogWarning(message);
                    break;
                case JamLogLevel.Error:
                case JamLogLevel.Fatal:
                    Debug.LogError(message);
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }
    }
}
