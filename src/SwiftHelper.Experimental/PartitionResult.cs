using System.Collections.Generic;

namespace SwiftHelper.Experimental
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