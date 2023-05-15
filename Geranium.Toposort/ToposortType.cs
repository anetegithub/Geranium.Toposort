using System;
using System.Collections.Generic;

namespace Geranium
{
    /// <summary>
    /// Base class for topologically sortable objects
    /// </summary>
    public class ToposortType : IToposort
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual int Weight => 0;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual string Id => this.GetType().IsGenericType
            ? this.GetType().GetGenericTypeDefinition().FullName
            : this.GetType().FullName;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<string> Dependencies { get; } = new List<string>();

        /// <inheritdoc cref="InitDependencies"/>
        protected virtual void SetDependencies() { }

        /// <summary>
        /// Set dependency of <see cref="Type"/>
        /// </summary>
        /// <typeparam name="T">Object implemented <see cref="IToposort"/></typeparam>
        protected void DependsOn<T>() where T : IToposort
        {
            Dependencies.Add(typeof(T).FullName);
        }

        /// <summary>
        /// Set dependency of id
        /// </summary>
        protected void DependsOn(string id)
        {
            Dependencies.Add(id);
        }

        /// <summary>
        /// Set dependency of generic type
        /// </summary>
        /// <param name="typeofOpenGeneric"></param>
        protected void DepensOnOpenGeneric(Type typeofOpenGeneric)
        {
            if (typeofOpenGeneric.IsGenericType)
            {
                Dependencies.Add(typeofOpenGeneric.GetGenericTypeDefinition().FullName);
            }
            else
            {
                Dependencies.Add(typeofOpenGeneric.FullName);
            }
        }

        public void InitDependencies()
        {
            SetDependencies();
        }
    }
}