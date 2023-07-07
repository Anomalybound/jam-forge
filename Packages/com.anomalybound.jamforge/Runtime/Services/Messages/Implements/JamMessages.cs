using System;
using System.Collections.Generic;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace JamForge
{
    [UnityEngine.Scripting.Preserve]
    public class JamMessages : IJamMessages
    {
        private LifetimeScope JamMessageScope { get; set; }

        private readonly List<Action<IContainerBuilder>> _brokerRegistrations = new();
        private readonly MessagePipeOptions _messagePipeOptions = new();

        public void RegisterFor<TMessage>()
        {
            var action = new Action<IContainerBuilder>(builder =>
            {
                builder.RegisterMessageBroker<TMessage>(_messagePipeOptions);
            });
            _brokerRegistrations.Add(action);
        }

        public void UnregisterFor<TMessage>()
        {
            var action = new Action<IContainerBuilder>(builder =>
            {
                builder.RegisterMessageBroker<TMessage>(_messagePipeOptions);
            });
            _brokerRegistrations.Remove(action);
        }

        public void Build()
        {
            if (JamMessageScope != null)
            {
                JamMessageScope.Dispose();
                JamMessageScope = null;
            }

            var messageScope = LifetimeScope.Create(RegisterBrokers);
            JamMessageScope = messageScope;
            Jam.OverrideResolver(JamMessageScope.Container);
        }

        private void RegisterBrokers(IContainerBuilder builder)
        {
            for (var i = 0; i < _brokerRegistrations.Count; i++)
            {
                var registration = _brokerRegistrations[i];
                registration.Invoke(builder);
            }

            _brokerRegistrations.Clear();
        }
    }
}
