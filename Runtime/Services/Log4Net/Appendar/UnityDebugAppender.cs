using log4net.Appender;
using log4net.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace JamForge.Log4Net
{
    [Preserve]
    public class UnityDebugAppender : AppenderSkeleton
    {
        [HideInCallstack]
        protected override void Append(LoggingEvent loggingEvent)
        {
            var level = loggingEvent.Level;
            
            if (Level.Fatal.Equals(level) || Level.Error.Equals(level))
            {
                Debug.LogError(RenderLoggingEvent(loggingEvent));
            }
            else if (Level.Warn.Equals(level))
            {
                Debug.LogWarning(RenderLoggingEvent(loggingEvent));
            }
            else
            {
                Debug.Log(RenderLoggingEvent(loggingEvent));
            }
        }
    }
}
