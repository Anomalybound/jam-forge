using JamForge;
using UnityEngine.Scripting;

[Preserve]
public class MainProcedure : ProcedureBase
{
    private TestServices TestServices { get; }

    public MainProcedure(TestServices testServices)
    {
        TestServices = testServices;
    }

    protected override void OnEnter()
    {
        Jam.Logger.Debug($"Main procedure entered: -> {TestServices.GetText()}");
    }

    protected override void OnExit()
    {
        Jam.Logger.Debug($"Main procedure exited.");
    }
}
