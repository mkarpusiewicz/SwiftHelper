using System.Collections.Generic;

namespace SwiftHelper
{
    public static partial class Extensions
    {
        /// <summary>
        ///     Holds collections of added, removed and modified elements that are the result of comparison of two collections.
        /// </summary>
        /// <typeparam name="T">Type of items in the collections</typeparam>
        public class CompareResult<T> : SimpleCompareResult<T>
        {
            internal CompareResult(IReadOnlyCollection<T> added, IReadOnlyCollection<T> removed, IReadOnlyCollection<T> modified)
                : base(added, removed)
            {
                Modified = modified;
            }

            public IReadOnlyCollection<T> Modified { get; }
        }
    }
}