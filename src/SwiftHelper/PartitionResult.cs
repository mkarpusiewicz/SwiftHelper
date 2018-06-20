using System.Collections.Generic;

namespace SwiftHelper
{
    public static partial class Extensions
    {
        /// <summary>
        ///     Holds collections of elements that satisfy (True) or not satisfy (False) the predicate on the source collection.
        /// </summary>
        /// <typeparam name="T">Type of items in the collections</typeparam>
        public class PartitionResult<T>
        {
            internal PartitionResult(IReadOnlyCollection<T> @true, IReadOnlyCollection<T> @false)
            {
                True = @true;
                False = @false;
            }

            public IReadOnlyCollection<T> True { get; }
            public IReadOnlyCollection<T> False { get; }
        }
    }
}