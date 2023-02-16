using System;
using JamForge.Events;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace JamForge
{
    [DefaultExecutionOrder(-10000)]
    public class JFCoreScope : LifetimeScope
    {
        private static Log4NetWrapper LogWrapper => Log4NetWrapper.Current;
        private static ILog Log => LogWrapper.GetLog(nameof(JFCoreScope));

        protected override void Configure(IContainerBuilder builder)
        {
            // Event broker
            builder.RegisterComponentOnNewGameObject<MainThreadDispatcher>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EventBroker>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

            // Log4Net
            builder.RegisterInstance(LogWrapper).AsImplementedInterfaces().AsSelf();

            Log.Debug($"JamForge core services registered!");
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Initialize()
        {
            var jfVersionControl = Jam.Resolver.Resolve<JFVersionControl>();
            Jam.Logger.Debug($"JamForge initialized! Version: {jfVersionControl.Version}".DyeCyan());
        }
    }
}
