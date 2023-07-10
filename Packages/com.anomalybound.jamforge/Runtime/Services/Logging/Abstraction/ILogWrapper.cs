using System;

namespace JamForge.Logging
{
    public enum JamLogLevel
    {
        Trace,
        Debug,
        Info,
        Warn,
        Error,
        Fatal,
    }

    public interface ILogger
    {
        void T(string message);
        void D(string message);
        void I(string message);
        void W(string message);
        void E(string message);
        void F(string message);
    }

    public interface ILogManager
    {
        public void SetGlobalLogLevel(JamLogLevel logLevel);

        public void AddAppender(LogDelegate appender);

        public void RemoveAppender(LogDelegate appender);

        public void ClearAppenders();

        public ILogger GetLogger<T>();

        public ILogger GetLogger(Type type);

        public ILogger GetLogger(string name);
    }
}
