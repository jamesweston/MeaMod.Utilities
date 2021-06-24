using System;
using System.Collections.Generic;

namespace MeaMod.Utilities.NetworkTools.Extensions
{
    /// <summary>
    /// Extension class for <see cref="T:System.Collections.IEnumerable" />.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Runs the specified <paramref name="code" /> for each item in the collection.
        /// </summary>
        /// <typeparam name="TItem">The type on which the enumerable is based.</typeparam>
        /// <param name="instance">The instance of the enumerable to process.</param>
        /// <param name="code">The code to execute for each item in the enumerable.</param>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        /// <returns>The original enumerable, for more processing.</returns>
        public static IEnumerable<TItem> Each<TItem>(
          this IEnumerable<TItem> instance,
          Action<TItem> code)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));
            if (code == null)
                throw new ArgumentNullException(nameof(code));
            foreach (TItem obj in instance)
                code(obj);
            return instance;
        }
    }
}
