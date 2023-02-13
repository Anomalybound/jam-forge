using JamForge.Events;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace JamForge
{
    public partial class Jam
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Reset()
        {
            _instance = null;
            _facadeScope = null;
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
