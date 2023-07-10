namespace JamForge.Logging
{
    public delegate string LogFormatter(JamLogger logger, JamLogLevel logLevel, string message);

    public interface ILogFormatter
    {
        string FormatMessage(JamLogger logger, JamLogLevel logLevel, string message);
    }
}
