using JamForge.Events;
using JamForge.Procedures;
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
            if (_facadeScope)
            {
                Object.Destroy(_facadeScope.gameObject);
            }

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

                if (!_facadeScope)
                {
                    _facadeScope = LifetimeScope.Create(FacadeScopeConstruction);
                    _facadeScope.hideFlags = HideFlags.HideInHierarchy;

                    var jfVersion = Resolver.Resolve<JFVersionControl>();
                    Resolver.Resolve<ProcedureManager>();
                    
                    Logger.Debug($"JamForge initialized! Current version: {jfVersion.Version}".DyeCyan());
                }

                _instance = _facadeScope.Container.Resolve<Jam>();

                return _instance;
            }
        }

        private static void FacadeScopeConstruction(IContainerBuilder builder)
        {
            builder.Register<Jam>(Lifetime.Singleton);
            builder.Register<JFVersionControl>(Lifetime.Singleton);
            
            builder.RegisterComponentOnNewGameObject<ProcedureManager>(Lifetime.Singleton);
        }

        #region Events

        [Inject]
        private IEventBrokerFacade _eventBroker;

        public static IEventBrokerFacade Events => Instance._eventBroker;

        #endregion
    }
}
