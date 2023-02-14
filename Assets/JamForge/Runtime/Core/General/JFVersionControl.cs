using UnityEngine;

namespace JamForge
{
    public class JFVersionControl : MonoBehaviour
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public const string Version = "0.0.1";

        private void Start()
        {
            Jam.Logger.Debug($"JamForge initialized! Current version: {Version}".DyeCyan());
        }
    }
}
