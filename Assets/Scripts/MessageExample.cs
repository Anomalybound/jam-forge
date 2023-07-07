using JamForge;
using MessagePipe;
using UnityEngine;

namespace DefaultNamespace
{
    public class MessageExample : MonoBehaviour
    {
        private void Start()
        {
            Jam.Messages.RegisterFor<GameStartMessage>();
            Jam.Messages.Build();

            var subscriber = Jam.Resolver.Resolve<ISubscriber<GameStartMessage>>();
            subscriber.Subscribe(OnGameStarted);
        }

        public void OnGameStarted(GameStartMessage value)
        {
            Jam.Logger.D($"Received GameStartMessage: {value.GameName}");
        }
    }
}
