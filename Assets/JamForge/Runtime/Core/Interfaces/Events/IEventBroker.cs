using System;
using Cysharp.Threading.Tasks;

namespace JamForge.Events
{
    public interface IEventBroker
    {
        #region Register

        void Register(object obj);

        void Unregister(object obj);

        void Register<TEvent>(Action<TEvent> action, short priority = 0,
            ThreadMode threadMode = ThreadMode.Current) where TEvent : Payloads;

        void Register<TEvent>(string endpoint, Action<TEvent> action,
            short priority = 0, ThreadMode threadMode = ThreadMode.Current) where TEvent : Payloads;

        void Unregister<TEvent>(string endpoint, Action<TEvent> action) where TEvent : Payloads;

        void Unregister<TEvent>(Action<TEvent> action) where TEvent : Payloads;

        #endregion

        #region Fire Events

        void Fire(string endpoint);

        void Fire<TEventData>(TEventData payloads)
            where TEventData : Payloads;

        void Fire<TEventData>(string endpoint, TEventData payloads)
            where TEventData : Payloads;

        #endregion
    }

    public interface IStickyEventBroker
    {
        void FireSticky(string endpoint);

        void FireSticky<TEventData>(TEventData payloads)
            where TEventData : Payloads;

        void FireSticky<TEventData>(string endpoint, TEventData payloads)
            where TEventData : Payloads;
    }

    public interface IAsyncEventBroker
    {
        UniTask FireAsync(string endpoint);

        UniTask FireAsync<TEventData>(TEventData payloads)
            where TEventData : Payloads;

        UniTask FireAsync<TEventData>(string endpoint, TEventData payloads)
            where TEventData : Payloads;
    }
}
