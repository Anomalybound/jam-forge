using UnityEngine.Scripting;

namespace JamForge
{
    [Preserve]
    public class JFVersionControl
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public string Version => "0.0.1";

        public void PrintInitializationMessage()
        {
            Jam.Logger.Debug($"JamForge initialized! Current version: {Version}".DyeCyan());
        }
    }
}
