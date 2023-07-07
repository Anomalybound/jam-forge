using System.IO;
using log4net.Appender;
using UnityEngine;
using UnityEngine.Scripting;

namespace JamForge.Log4Net
{
    [Preserve]
    public class UnityRollingFileAppender : RollingFileAppender
    {
        public override string File
        {
            set
            {
                var path = Path.Combine(Application.isEditor ? Directory.GetCurrentDirectory() : Application.temporaryCachePath, "Logs");
                base.File = Path.Combine(path, value);
            }
        }
    }
}
