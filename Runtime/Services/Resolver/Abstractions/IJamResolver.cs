using System;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace JamForge
{
    public interface IJamResolver
    {
        public void EnqueueScope(LifetimeScope scope);

        public object Create(Type type);

        public T Create<T>() where T : class;

        public T Resolve<T>();

        public object Resolve(Type type);

        public void Inject<T>(T instance);

        public void InjectGameObject(GameObject gameObject);

        public T Instantiate<T>(T prefab) where T : Object;

        public T Instantiate<T>(T prefab, Transform parent) where T : Object;

        public T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation) where T : Object;

        public T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : Object;
    }
}
