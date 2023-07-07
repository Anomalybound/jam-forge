using JamForge.Events;
using JamForge.Log4Net;
using JamForge.Serialization;
using JamForge.Store;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace JamForge
{
    [DefaultExecutionOrder(-10000)]
    public class JamForgeScoreScope : LifetimeScope
    {
        [SerializeField] private TextAsset packageJson;

        protected override void Configure(IContainerBuilder builder)
        {
            // Event broker
            builder.RegisterComponentOnNewGameObject<MainThreadDispatcher>(Lifetime.Singleton).AsImplementedInterfaces();
            // Includes IEventBrokerFacade, IAsyncEventBroker, IStickyEventBroker, IEventBroker
            builder.Register<EventBroker>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

            // MessagePipe
            builder.RegisterMessagePipe(cfg =>
            {
                cfg.RequestHandlerLifetime = InstanceLifetime.Singleton;
                cfg.DefaultAsyncPublishStrategy = AsyncPublishStrategy.Parallel;
            });
            builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));

            // Log4Net
            builder.Register<ILogWrapper, Log4NetWrapper>(Lifetime.Singleton);

            // Serialization
            builder.Register<IJsonSerializer, CatJsonSerializer>(Lifetime.Singleton);
            builder.Register<IBinarySerializer, NaniBinarySerializer>(Lifetime.Singleton);

            // Store
            builder.Register<IPersistStoreVendor, PlayerPrefsStoreVendor>(Lifetime.Singleton);
            builder.Register<IMemoryStoreVendor, InMemoryDictionaryStore>(Lifetime.Singleton);
            builder.Register<PlayerPrefsStoreVendor.PlayerPrefsPersistStore>(Lifetime.Transient);
            builder.Register<InMemoryDictionaryStore.DictionaryMemoryStore>(Lifetime.Transient);
            builder.Register<IJamStores, JamStores>(Lifetime.Singleton);

            // Message
            builder.Register<IJamMessages, JamMessages>(Lifetime.Singleton);

            // Resolver
            builder.Register<IJamResolver, JamResolver>(Lifetime.Singleton);

            // Config
            var config = Resources.Load<JamForgeConfig>(nameof(JamForgeConfig));
            if (config) { builder.RegisterInstance(config); }

            // Package info
            var packageInfo = JsonUtility.FromJson<PackageInfo>(packageJson.text);
            if (packageInfo != null) { builder.RegisterInstance(packageInfo); }

            // Services
            builder.Register<IJamServices, JamServices>(Lifetime.Singleton);

            JFLog.Info($"JamForge core services registered!");

            builder.RegisterBuildCallback(OnCoreServicesRegistered);
        }

        private void OnCoreServicesRegistered(IObjectResolver resolver)
        {
            var packageInfo = resolver.Resolve<PackageInfo>();
            var version = packageInfo.Version;
            JFLog.Debug($"JamForge initialized! Version: {version}".DyeCyan());
        }
    }
}
