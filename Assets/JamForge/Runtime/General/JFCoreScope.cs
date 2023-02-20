using JamForge.Events;
using JamForge.Log4Net;
using JamForge.Serialization;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace JamForge
{
    [DefaultExecutionOrder(-10000)]
    public class JFCoreScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // Event broker
            builder.RegisterComponentOnNewGameObject<MainThreadDispatcher>(Lifetime.Singleton).AsImplementedInterfaces();
            // Includes IEventBrokerFacade, IAsyncEventBroker, IStickyEventBroker, IEventBroker
            builder.Register<EventBroker>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

            // Log4Net
            builder.Register<ILogWrapper, Log4NetWrapper>(Lifetime.Singleton);

            // Serialization
            builder.Register<IJsonSerializer, CatJsonSerializer>(Lifetime.Singleton);

            JFLog.Debug($"JamForge core services registered!");
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Initialize()
        {
            var jfVersionControl = Jam.Resolver.Resolve<JFVersionControl>();
            JFLog.Debug($"JamForge initialized! Version: {jfVersionControl.Version}".DyeCyan());
        }
    }
}
