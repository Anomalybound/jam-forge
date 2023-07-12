using JamForge;
using MessagePipe;
using UnityEngine;

public class MessageExample : MonoBehaviour
{
    private void Start()
    {
        var subscriber = Jam.Resolver.Resolve<ISubscriber<GameStartMessage>>();
        subscriber.Subscribe(OnGameStarted);
    }

    public void OnGameStarted(GameStartMessage value)
    {
        Jam.Logger.Debug($"Received GameStartMessage: {value.GameName}");
    }
}