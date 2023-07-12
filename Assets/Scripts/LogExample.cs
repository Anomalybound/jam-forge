using JamForge;
using UnityEngine;

public class LogExample : MonoBehaviour
{
    private void Start()
    {
        var logger = Jam.LogManager.GetLogger<LogExample>();
        logger.Trace("Trace");
        logger.Debug("Debug");
        logger.Info("Info");
        logger.Warn("Warn");
        logger.Error("Error");
        logger.Fatal("Fatal");
    }
}
