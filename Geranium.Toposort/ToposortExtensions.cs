namespace Geranium
{
    /// <summary>
    /// Методы расширений для работы топологической сортировки
    /// </summary>
    public static class ToposortExtensions
    {
        public static IEnumerable<T> Sort<T>(this IEnumerable<T> elements, Toposorting<T> toposorting=null)
            where T : IToposort
        => TopoSort(elements, toposorting);

        public static IEnumerable<T> TopoSort<T>(this IEnumerable<T> elements, Toposorting<T> toposorting=null)
            where T : IToposort
        {
            toposorting ??= ToposortingDefault.GetDefault<T>();

            if (elements == null)
                return Enumerable.Empty<T>();

            var independant = elements
                .Where(NoDeps)
                .OrderBy(x => x.Weight);

            var sorted = toposorting(elements);

            return independant.Concat(sorted.Where(x=>!NoDeps(x)));
        }

        private static bool NoDeps<T>(T element)
            where T : IToposort
        => element.Dependencies == default || element.Dependencies.Count == 0;
    }
}