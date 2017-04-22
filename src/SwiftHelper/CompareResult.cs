using System.Collections.Generic;

namespace SwiftHelper
{
    public static partial class Extensions
    {
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