using System.Collections.Generic;

namespace SwiftHelper
{
    public static partial class Extensions
    {
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