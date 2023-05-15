using System.Collections.Generic;

namespace Geranium
{
    /// <summary>
    /// Interface for objects sorted according to dependencies from each other
    /// </summary>
    public interface IToposort
    {
        /// <summary>
        /// Object weight, if no dependency
        /// </summary>
        int Weight { get; }

        /// <summary>
        /// Object identifier
        /// </summary>
        string Id { get; }

        /// <summary>
        /// List of object dependencies id's
        /// </summary>
        List<string> Dependencies { get; }

        /// <summary>
        /// Method for initializing dependencies
        /// </summary>
        void InitDependencies();
    }
}