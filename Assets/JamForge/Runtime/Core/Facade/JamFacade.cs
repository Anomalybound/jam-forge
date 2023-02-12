using System;
using JamForge.Events;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;
using VContainer.Unity;

namespace JamForge
{
    public class Jam
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Reset()
        {
            _instance = null;
            _facadeScope = null;
        }

        public class ResolverWrapper
        {
            private readonly IObjectResolver _resolver;

            public ResolverWrapper(IObjectResolver resolver)
            {
                _resolver = resolver;
            }

            public T Resolve<T>()
            {
                return _resolver.Resolve<T>();
            }

            public object Resolve(Type type)
            {
                return _resolver.Resolve(type);
            }

            public T Inject<T>(T instance)
            {
                _resolver.Inject(instance);
                return instance;
            }
        }

        private static LifetimeScope _facadeScope;

        private static Jam _instance;

        private static Jam Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                _facadeScope = LifetimeScope.Create(FacadeScopeConstruction);
                _facadeScope.name = "JamForgeFacadeScope";
                _instance = _facadeScope.Container.Resolve<Jam>();

                return _instance;
            }
        }

        private static void FacadeScopeConstruction(IContainerBuilder builder)
        {
            builder.Register<Jam>(Lifetime.Singleton);

            Debug.Log($"Facade scope registered!");
        }

        #region Services

        [Inject]
        private IEventBrokerFacade _eventBroker;

        [Inject]
        private IObjectResolver _objectResolver;

        private ResolverWrapper _resolverInstance;

        private ResolverWrapper ResolverInstance
        {
            get
            {
                if (_resolverInstance != null)
                {
                    return _resolverInstance;
                }

                _resolverInstance = new ResolverWrapper(_objectResolver);
                return _resolverInstance;
            }
        }

        public static IEventBrokerFacade Events => Instance._eventBroker;

        public static ResolverWrapper Resolver => Instance.ResolverInstance;

        #endregion
    }
}
