using JamForge;
using MessagePipe;
using UnityEngine;

namespace DefaultNamespace
{
    public class MessageExample : MonoBehaviour
    {
        private void Start()
        {
            Jam.Messages.RegisterFor<float>();
            Jam.Messages.Build();

            var subscriber = Jam.Resolver.Resolve<ISubscriber<float>>();
            subscriber.Subscribe(OnFloatChanged);
            var publisher = Jam.Resolver.Resolve<IPublisher<float>>();
            publisher.Publish(50);
        }

        public void OnFloatChanged(float value)
        {
            Jam.Logger.D($"Received float: {value}");
        }
    }
}
