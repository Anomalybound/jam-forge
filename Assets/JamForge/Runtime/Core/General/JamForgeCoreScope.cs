using JamForge.Events;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace JamForge
{
    [DefaultExecutionOrder(-10000)]
    public class JamForgeCoreScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // Event broker
            builder.RegisterComponentOnNewGameObject<MainThreadDispatcher>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EventBroker>(Lifetime.Singleton).AsImplementedInterfaces();

            Debug.Log($"Core scope registered!");
        }
    }
}
