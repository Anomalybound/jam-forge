using JamForge.Logging;
using UnityEngine;
using VContainer;
using ILogger = JamForge.Logging.ILogger;

namespace JamForge
{
    internal static class InternalLog
    {
        internal static ILogger Logger { get; set; }
        
#if UNITY_2022_2_OR_NEWER
        [HideInCallstack]
#endif
        public static void T(string message) => Logger.T(message);

#if UNITY_2022_2_OR_NEWER
        [HideInCallstack]
#endif
        public static void D(string message) => Logger.D(message);

#if UNITY_2022_2_OR_NEWER
        [HideInCallstack]
#endif
        public static void I(string message) => Logger.I(message);

#if UNITY_2022_2_OR_NEWER
        [HideInCallstack]
#endif
        public static void W(string message) => Logger.W(message);

#if UNITY_2022_2_OR_NEWER
        [HideInCallstack]
#endif
        public static void E(string message) => Logger.E(message);

#if UNITY_2022_2_OR_NEWER
        [HideInCallstack]
#endif
        public static void F(string message) => Logger.F(message);
    }

    public partial class Jam
    {
        #region Logs

        [Inject]
        private ILogger _logger;

        [Inject]
        private ILogManager _logManager;

        public static ILogger Logger => Instance._logger;

        public static ILogManager LogManager => Instance._logManager;

        #endregion
    }
}
