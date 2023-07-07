using log4net.Core;
using UnityEngine;

namespace JamForge
{
    public class JamForgeConfig : ScriptableObject
    {
        [SerializeField] private Level logLevel = Level.Debug;

        public Level LOGLevel => logLevel;
    }
}
