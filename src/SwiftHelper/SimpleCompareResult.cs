using System.Collections.Generic;

namespace SwiftHelper
{
    public static partial class Extensions
    {
        /// <summary>
        ///     Holds collections of added and removed elements that are the result of comparison of two collections.
        /// </summary>
        /// <typeparam name="T">Type of items in the collections</typeparam>
        public class SimpleCompareResult<T>
        {
            internal SimpleCompareResult(IReadOnlyCollection<T> added, IReadOnlyCollection<T> removed)
            {
                Added = added;
                Removed = removed;
            }

            public IReadOnlyCollection<T> Added { get; }
            public IReadOnlyCollection<T> Removed { get; }
        }
    }
}