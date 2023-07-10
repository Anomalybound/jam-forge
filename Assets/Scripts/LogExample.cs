using JamForge;
using UnityEngine;

public class LogExample : MonoBehaviour
{
    private void Start()
    {
        var logger = Jam.LogManager.GetLogger<LogExample>();
        logger.T("Trace");
        logger.D("Debug");
        logger.I("Info");
        logger.W("Warn");
        logger.E("Error");
        logger.F("Fatal");
    }
}
