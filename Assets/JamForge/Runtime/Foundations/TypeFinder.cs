using System;
using System.Collections.Generic;
using System.Reflection;

namespace JamForge
{
    public static class TypeFinder
    {
        private static readonly Dictionary<string, Type> TypeCaches = new();

        private static readonly List<Assembly> Assemblies = new();

        private static Type GetTypeInAllAssemblies(string typeName)
        {
            if (Assemblies.Count == 0)
            {
                Assemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies());
            }

            for (var i = 0; i < Assemblies.Count; i++)
            {
                var assembly = Assemblies[i];
                var type = assembly.GetType(typeName);
                if (type != null)
                {
                    return type;
                }
            }

            return null;
        }

        public static Type Get(string typeName)
        {
            // Check if it's cached
            if (TypeCaches.TryGetValue(typeName, out var type))
            {
                return type;
            }

            // Check if it's in the current assembly
            type = Type.GetType(typeName);
            if (type != null)
            {
                TypeCaches.Add(typeName, type);
                return type;
            }

            // Check if it's in all assemblies
            type = GetTypeInAllAssemblies(typeName);
            if (type != null)
            {
                TypeCaches.Add(typeName, type);
                return type;
            }

            // Not a valid type
            return null;
        }
    }
}
