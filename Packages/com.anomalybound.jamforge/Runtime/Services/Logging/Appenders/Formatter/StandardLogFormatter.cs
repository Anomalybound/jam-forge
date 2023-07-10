using UnityEngine.Scripting;

namespace JamForge.Logging
{
    [Preserve]
    public class StandardLogFormatter : ILogFormatter
    {
        private readonly string _format;
        
        private const string TraceColor = "#85B7B1";
        private const string DebugColor = "#917A98";
        private const string InfoColor = "#AFB866";
        private const string WarnColor = "#E6C071";
        private const string ErrorColor = "#CC6766";
        private const string FatalColor = "magenta";

        public StandardLogFormatter(string format = "[{1}] {0}: {2}") => _format = format;
        
        public static string GetColoredLevel(JamLogLevel logLevel)
        {
            switch (logLevel)
            {
                case JamLogLevel.Trace:
                    return logLevel.ToString().Colorize(TraceColor);
                case JamLogLevel.Debug:
                    return logLevel.ToString().Colorize(DebugColor);
                case JamLogLevel.Info:
                    return logLevel.ToString().Colorize(InfoColor);
                case JamLogLevel.Warn:
                    return logLevel.ToString().Colorize(WarnColor);
                case JamLogLevel.Error:
                    return logLevel.ToString().Colorize(ErrorColor);
                case JamLogLevel.Fatal:
                    return logLevel.ToString().Colorize(FatalColor);
                default:
                    return logLevel.ToString();
            }
        }

        public string FormatMessage(JamLogger logger, JamLogLevel logLevel, string message)
        {
            return string.Format(_format, logger.Name, GetColoredLevel(logLevel), message);
        }
    }
}
