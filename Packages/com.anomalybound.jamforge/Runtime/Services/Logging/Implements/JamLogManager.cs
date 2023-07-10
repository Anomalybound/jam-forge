using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace JamForge.Logging
{
    [Preserve]
    public class JamLogManager : ILogManager
    {
        public JamLogLevel GlobalLogLevel
        {
            get => _globalLogLevel;
            set
            {
                _globalLogLevel = value;
                foreach (var logger in _loggers.Values) { logger.LogLevel = value; }
            }
        }

        private readonly Dictionary<string, JamLogger> _loggers = new();

        private JamLogLevel _globalLogLevel;
        private LogDelegate _appenders;

        public void SetGlobalLogLevel(JamLogLevel logLevel)
        {
            _globalLogLevel = logLevel;
            foreach (var logger in _loggers.Values)
            {
                logger.LogLevel = logLevel;
            }
        }

        public void AddAppender(LogDelegate appender)
        {
            _appenders += appender;
            foreach (var logger in _loggers.Values)
            {
                logger.LogDelegate += appender;
            }
        }

        public void RemoveAppender(LogDelegate appender)
        {
            _appenders -= appender;
            foreach (var logger in _loggers.Values)
            {
                logger.LogDelegate -= appender;
            }
        }

        public ILogger GetLogger<T>() => GetLogger(typeof(T));

        public ILogger GetLogger(Type type) => GetLogger(type.FullName);

        public ILogger GetLogger(string name)
        {
            if (_loggers.TryGetValue(name, out var logger)) { return logger; }

            logger = new JamLogger
            {
                Name = name,
                LogLevel = _globalLogLevel
            };
            logger.LogDelegate += _appenders;
            
            _loggers.Add(name, logger);
            return logger;
        }

        public void ClearLoggers() => _loggers.Clear();

        public void ClearAppenders()
        {
            _appenders = null;
            foreach (var logger in _loggers.Values)
            {
                logger.SetAppender(null);
            }
        }
    }
}
