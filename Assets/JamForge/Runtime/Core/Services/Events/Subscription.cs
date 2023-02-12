using System;
using System.Linq;
using System.Reflection;

namespace JamForge.Events
{
    public readonly struct Subscription : IComparable<Subscription>, IEquatable<Subscription>
    {
        public string Endpoint { get; }

        public short Priority { get; }

        public ThreadMode ThreadMode { get; }

        public Type EventDataType { get; }

        public string MethodName { get; }

        public Delegate MethodInvoker { get; }

        public static Subscription CreateFromAction<TEventData>(string endpoint, Action<TEventData> action)
        {
            return new Subscription(endpoint, 0, ThreadMode.Current, action.Method, action.Target);
        }

        public static Subscription CreateFromAction<TEventData>(string endpoint, Action<TEventData> action,
            short priority, ThreadMode threadMode)
        {
            return new Subscription(endpoint, priority, threadMode, action.Method, action.Target);
        }

        public Subscription(string endpoint, short priority, ThreadMode threadMode,
            MethodInfo methodInfo, object target)
        {
            Endpoint = endpoint;
            Priority = priority;
            ThreadMode = threadMode;

            MethodName = methodInfo.Name;

            var parameters = methodInfo.GetParameters();
            if (parameters.Length <= 0)
            {
                EventDataType = null;
                MethodInvoker = methodInfo.CreateDelegate(target);
            }
            else
            {
                EventDataType = methodInfo.GetParameters()[0].ParameterType;
                MethodInvoker = methodInfo.CreateDelegate(target);
            }
        }

        public int CompareTo(Subscription other)
        {
            return other.Priority.CompareTo(Priority);
        }

        public bool Equals(Subscription other)
        {
            return Endpoint == other.Endpoint && EventDataType == other.EventDataType && MethodName == other.MethodName;
        }

        public override bool Equals(object obj)
        {
            return obj is Subscription other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Endpoint, Priority, (int)ThreadMode, EventDataType, MethodName, MethodInvoker);
        }
    }
}
