using System.Collections.Generic;

namespace SwiftHelper
{
    public static partial class Extensions
    {
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