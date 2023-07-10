namespace JamForge.Logging
{
    public delegate void LogDelegate(JamLogger logger, JamLogLevel logLevel, string message);
    
    public interface ILogDelegate
    {
        void WriteLine(JamLogger logger, JamLogLevel logLevel, string message);
    }
}
