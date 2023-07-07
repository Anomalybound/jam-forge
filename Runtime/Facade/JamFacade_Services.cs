using VContainer;

namespace JamForge
{
    public partial class Jam
    {
        #region Resolver

        [Inject]
        private IJamServices _services;

        public static IJamServices Services => Instance._services;

        #endregion
    }
}
