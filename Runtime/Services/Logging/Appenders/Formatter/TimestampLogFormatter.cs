using System;
using System.Globalization;
using UnityEngine.Scripting;

namespace JamForge.Logging
{
    [Preserve]
    public class TimestampLogFormatter : ILogFormatter
    {
        public string FormatMessage(JamLogger logger, JamLogLevel logLevel, string message)
        {
            return $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)} {message}";
        }
    }
}
