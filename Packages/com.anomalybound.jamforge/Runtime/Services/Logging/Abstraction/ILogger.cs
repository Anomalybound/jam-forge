﻿namespace JamForge.Logging
{
    public interface ILogger
    {
        void Assert(bool condition, string message);
        void Trace(string message);
        void Debug(string message);
        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Fatal(string message);
    }
}
