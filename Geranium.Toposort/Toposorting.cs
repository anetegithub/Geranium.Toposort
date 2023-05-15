using System;
using System.Collections.Generic;
using System.Linq;

namespace Geranium
{
    public delegate TToposort[] Toposorting<TToposort>(IEnumerable<TToposort> @enum)
        where TToposort : IToposort;

    public static class ToposortingDefault
    {
        public static Toposorting<TToposort> GetDefault<TToposort>()
        where TToposort : IToposort
        => elements =>
        {
            var cycled = false;
            var result = new List<TToposort>();
            var seen = new Dictionary<TToposort, ToposortResult>();

            var map = new Dictionary<string, TToposort>();

            void BuildMap()
            {
                foreach (var element in elements)
                {
                    if (element.Dependencies.Count == 0)
                        element.InitDependencies();

                    if (map.ContainsKey(element.Id))
                        continue;

                    map.Add(element.Id, element);
                }
            }
            BuildMap();

            IEnumerable<TToposort> GetDepsFromMap(TToposort toposort)
            {
                if(!map.ContainsKey(toposort.Id))
                    return Array.Empty<TToposort>();

                return map
                    .Where(kv => toposort.Dependencies.Contains(kv.Key))
                    .Select(kv => kv.Value);
            }

            bool Topo(TToposort element)
            {
                if (seen.ContainsKey(element))
                {
                    switch (seen[element])
                    {
                        case ToposortResult.Seeing:
                                throw new InvalidOperationException($"A circular dependency was detected: {element.Id}!");
                        case ToposortResult.SeenIt:
                            return true;
                        case ToposortResult.Error:
                            return false;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                seen[element] = ToposortResult.Seeing;
                var deps = GetDepsFromMap(element);

                var ok = deps.All(Topo);

                if (ok)
                {
                    seen[element] = ToposortResult.SeenIt;
                    result.Add(element);
                }
                else
                {
                    seen[element] = ToposortResult.Error;
                }

                return ok;
            }

            foreach (var item in elements)
            {
                var walkResult = Topo(item);

                if (cycled)
                    throw new InvalidOperationException("A circular dependency was detected!");
            }

            return result.ToArray();
        };

        private enum ToposortResult
        {
            Seeing,
            SeenIt,
            Error
        };
    }
}