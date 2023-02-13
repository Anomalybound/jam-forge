using System;
using System.Collections.Generic;
using System.Linq;

namespace JamForge.Events
{
    public class EventNode
    {
        public string Endpoint { get; }

        public EventNode Parent { get; private set; }

        public Dictionary<string, EventNode> Children { get; } = new();

        public HashSet<Subscription> Subscriptions { get; private set; } = new();

        public EventNode(string endpoint)
        {
            Endpoint = endpoint;
        }

        private void SetParent(EventNode node)
        {
            Parent = node;
        }

        public void AddSubscription(Subscription subscription)
        {
            if (string.IsNullOrEmpty(subscription.Endpoint) || string.IsNullOrEmpty(subscription.MethodName))
            {
                throw new NullReferenceException();
            }

            Subscriptions.Add(subscription);
        }

        public void RemoveSubscription(Subscription subscription)
        {
            if (string.IsNullOrEmpty(subscription.Endpoint) || string.IsNullOrEmpty(subscription.MethodName))
            {
                throw new NullReferenceException();
            }

            // TODO: check performance issues
            Subscriptions = new HashSet<Subscription>(Subscriptions.Except(new[] { subscription }));
        }

        public void AddChild(EventNode node)
        {
            if (node == null) { throw new NullReferenceException(); }

            if (Children.TryAdd(node.Endpoint, node)) { node.SetParent(this); }
        }

        public static void AddSubscription(EventNode rootNode, Subscription subscription)
        {
            var targetNodes = rootNode.GetEventNodes(subscription.Endpoint);
            foreach (var targetNode in targetNodes) { targetNode.AddSubscription(subscription); }
        }

        public static void RemoveSubscription(EventNode rootNode, Subscription subscription)
        {
            var targetNodes = rootNode.GetEventNodes(subscription.Endpoint);
            foreach (var targetNode in targetNodes) { targetNode.RemoveSubscription(subscription); }
        }
    }
}
