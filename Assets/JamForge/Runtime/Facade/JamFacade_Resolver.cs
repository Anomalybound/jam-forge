using VContainer;

namespace JamForge
{
    public partial class Jam
    {
        #region Resolver

        [Inject]
        private IObjectResolver _objectResolver;

        [Inject]
        private IJamResolver _resolver;
        private IJamResolver _overrideResolver;

        internal static void OverrideResolver(IObjectResolver resolver)
        {
            Instance._overrideResolver = new JamResolver(resolver);
        }

        public static IJamResolver Resolver => Instance._overrideResolver ?? Instance._resolver;

        #endregion
    }
}
