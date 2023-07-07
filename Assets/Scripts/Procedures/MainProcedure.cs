using JamForge;
using UnityEngine.Scripting;

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
