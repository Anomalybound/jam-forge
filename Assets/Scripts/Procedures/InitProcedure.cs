using JamForge;
using MessagePipe;
using UnityEngine.Scripting;

[Preserve]
public class InitProcedure : ProcedureBase
{
    private IPublisher<GameStartMessage> GameStartPublisher { get; }

    public InitProcedure(IPublisher<GameStartMessage> gameStartPublisher)
    {
        GameStartPublisher = gameStartPublisher;
    }

    protected override void OnEnter()
    {
        GameStartPublisher.Publish(new GameStartMessage("Game Sample"));
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
