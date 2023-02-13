using System.Collections.Generic;

namespace JamForge.Events
{
    public static class EventNodeHelper
    {
        private const char Separator = '.';
        private const string Wildcard = "*";

        public static void EnsureEventNode(this EventNode rootNode, string path)
        {
            const int cursor = 0;
            var routes = path.Split(Separator);
            
            EnsureEventNode(rootNode, cursor, routes);
        }

        public static IEnumerable<EventNode> GetEventNodes(this EventNode rootNode, string path)
        {
            const int cursor = 0;
            var routes = path.Split(Separator);
            var results = new List<EventNode>();

            GetEventNodes(rootNode, cursor, routes, results);
            return results;
        }

        private static void GetEventNodes(EventNode rootNode, int cursor, IReadOnlyList<string> path, ICollection<EventNode> results)
        {
            if (cursor == path.Count)
            {
                results.Add(rootNode);
                return;
            }

            var route = path[cursor];
            var isWildcard = route.Equals(Wildcard);

            if (isWildcard)
            {
                foreach (var child in rootNode.Children.Values)
                {
                    GetEventNodes(child, cursor + 1, path, results);
                }
            }
            else
            {
                if (rootNode.Children.TryGetValue(route, out var child))
                {
                    GetEventNodes(child, cursor + 1, path, results);
                }
            }
        }

        private static void EnsureEventNode(EventNode rootNode, int cursor, IReadOnlyList<string> path)
        {
            if (cursor == path.Count) { return; }

            var route = path[cursor];
            var isWildcard = route.Equals(Wildcard);

            if (isWildcard)
            {
                foreach (var child in rootNode.Children.Values)
                {
                    EnsureEventNode(child, cursor + 1, path);
                }
            }
            else
            {
                if (!rootNode.Children.TryGetValue(route, out var child))
                {
                    child = new EventNode(route);
                    rootNode.AddChild(child);
                }

                EnsureEventNode(child, cursor + 1, path);
            }
        }

        private static void AddChildren(EventNode node, ICollection<EventNode> list)
        {
            foreach (var nodeChild in node.Children.Values)
            {
                list.Add(nodeChild);
                AddChildren(nodeChild, list);
            }
        }
    }
}
