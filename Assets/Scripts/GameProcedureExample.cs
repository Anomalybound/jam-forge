using JamForge;
using UnityEngine;
using UnityEngine.Scripting;

[Preserve]
public class InitProcedure : ProcedureBase
{
    protected override void OnEnter()
    {
        Jam.Logger.D($"Initial procedure entered.");
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (ElapsedTime >= 2f)
        {
            Jam.Logger.D($"Switching to main procedure.");
            GameProcedures.SwitchProcedure("MainProcedure");
        }
    }

    protected override void OnExit()
    {
        Jam.Logger.D($"Initial procedure exited.");
    }
}

[Preserve]
public class MainProcedure : ProcedureBase
{
    protected override void OnEnter()
    {
        Jam.Logger.D($"Main procedure entered.");
    }

    protected override void OnExit()
    {
        Jam.Logger.D($"Main procedure exited.");
    }
}

public class GameProcedureExample : MonoBehaviour { }
