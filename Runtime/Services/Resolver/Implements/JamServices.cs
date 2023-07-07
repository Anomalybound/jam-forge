using System;
using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

namespace JamForge
{
    public class JamServices : IJamServices
    {
        private LifetimeScope NewServicesScope { get; set; }

        private readonly List<Action<IContainerBuilder>> _registrations = new();

        public void Register<TContract, TImplementation>(ServiceLifetime lifetime = ServiceLifetime.Singleton)
            where TImplementation : TContract
        {
            var action = new Action<IContainerBuilder>(builder =>
            {
                builder.Register<TContract, TImplementation>((Lifetime)lifetime);
            });
            _registrations.Add(action);
        }

        public void Register<TImplementation>(ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            var action = new Action<IContainerBuilder>(builder =>
            {
                builder.Register<TImplementation>((Lifetime)lifetime);
            });
            _registrations.Add(action);
        }

        public void RegisterInstance<TInstance>(TInstance instance)
        {
            var action = new Action<IContainerBuilder>(builder =>
            {
                builder.RegisterInstance(instance);
            });
            _registrations.Add(action);
        }

        public void RegisterAll<TImplementation>(ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            var action = new Action<IContainerBuilder>(builder =>
            {
                builder.Register<TImplementation>((Lifetime)lifetime).AsImplementedInterfaces().AsSelf();
            });
            _registrations.Add(action);
        }

        public void Build()
        {
            if (NewServicesScope != null)
            {
                NewServicesScope.Dispose();
                NewServicesScope = null;
            }

            var servicesScope = LifetimeScope.Create(RegisterServices);
            NewServicesScope = servicesScope;
            Jam.OverrideResolver(NewServicesScope.Container);
        }

        private void RegisterServices(IContainerBuilder obj)
        {
            for (var i = 0; i < _registrations.Count; i++)
            {
                var registration = _registrations[i];
                registration.Invoke(obj);
            }

            _registrations.Clear();
        }
    }
}
