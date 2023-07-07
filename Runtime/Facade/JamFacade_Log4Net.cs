using JamForge.Log4Net;
using VContainer;

namespace JamForge
{
    public partial class Jam
    {
        #region Logs

        [Inject]
        private ILogWrapper _logger;

        public static ILogWrapper Logger => Instance._logger;

        #endregion
    }
}
