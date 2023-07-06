using VContainer;

namespace JamForge
{
    public partial class Jam
    {
        #region Messages

        [Inject]
        private IJamMessages _message;

        public static IJamMessages Messages => Instance._message;

        #endregion
    }
}
