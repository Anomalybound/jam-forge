using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace JamForge
{
    public partial class Jam
    {
        public class ResolverWrapper
        {
            private readonly IObjectResolver _resolver;

            public ResolverWrapper(IObjectResolver resolver)
            {
                _resolver = resolver;
            }

            [UnityEngine.Scripting.Preserve]
            public T Resolve<T>()
            {
                return _resolver.Resolve<T>();
            }

            [UnityEngine.Scripting.Preserve]
            public object Resolve(Type type)
            {
                return _resolver.Resolve(type);
            }

            [UnityEngine.Scripting.Preserve]
            public void Inject<T>(T instance)
            {
                _resolver.Inject(instance);
            }

            [UnityEngine.Scripting.Preserve]
            public void InjectGameObject(GameObject gameObject)
            {
                _resolver.InjectGameObject(gameObject);
            }

            [UnityEngine.Scripting.Preserve]
            public T Instantiate<T>(T prefab) where T : Object
            {
                return _resolver.Instantiate(prefab);
            }

            [UnityEngine.Scripting.Preserve]
            public T Instantiate<T>(T prefab, Transform parent) where T : Object
            {
                return _resolver.Instantiate(prefab, parent);
            }

            [UnityEngine.Scripting.Preserve]
            public T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation) where T : Object
            {
                return _resolver.Instantiate(prefab, position, rotation);
            }

            [UnityEngine.Scripting.Preserve]
            public T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : Object
            {
                return _resolver.Instantiate(prefab, position, rotation, parent);
            }
        }
    }
}
