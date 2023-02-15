using System;
using System.Collections.Generic;

namespace JamForge.Foundations
{
    public abstract class NodeData<TData>
    {
        public string Route { get; }

        public TData Data { get; }

        public NodeData<TData> Parent { get; private set; }

        public Dictionary<string, NodeData<TData>> Children { get; } = new();

        public NodeData(string route, TData data)
        {
            Route = route;
            Data = data;
        }

        private void SetParent(NodeData<TData> node)
        {
            Parent = node;
        }

        public void AddChild(NodeData<TData> node)
        {
            if (node == null) { throw new NullReferenceException(); }

            if (Children.TryAdd(node.Route, node))
            {
                node.SetParent(this);
            }
        }
    }
}
