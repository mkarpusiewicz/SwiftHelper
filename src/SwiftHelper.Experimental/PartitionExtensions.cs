using System;
using System.Collections.Generic;
using System.Linq;

namespace SwiftHelper.Experimental
{
    public static class PartitionExtensions
    {
        public static PartitionResult<TSource> PartitionLists<TSource>(this ICollection<TSource> source, Func<TSource, bool> predicate)
        {
            //if (source == null)
            //{
            //    throw new ArgumentNullException(nameof(source));
            //}
            //if (predicate == null)
            //{
            //    throw new ArgumentNullException(nameof(predicate));
            //}

            var trueList = new List<TSource>();
            var falseList = new List<TSource>();

            foreach (var element in source)
            {
                if (predicate(element))
                {
                    trueList.Add(element);
                }
                else
                {
                    falseList.Add(element);
                }
            }

            return new PartitionResult<TSource>(trueList, falseList);
        }

        public static PartitionResult<TSource> PartitionListsPresized<TSource>(this ICollection<TSource> source, Func<TSource, bool> predicate)
        {
            //if (source == null)
            //{
            //    throw new ArgumentNullException(nameof(source));
            //}
            //if (predicate == null)
            //{
            //    throw new ArgumentNullException(nameof(predicate));
            //}

            var trueList = new List<TSource>(source.Count);
            var falseList = new List<TSource>(source.Count);

            foreach (var element in source)
            {
                if (predicate(element))
                {
                    trueList.Add(element);
                }
                else
                {
                    falseList.Add(element);
                }
            }

            return new PartitionResult<TSource>(trueList, falseList);
        }

        public static PartitionResult<TSource> PartitionArray<TSource>(this ICollection<TSource> source, Func<TSource, bool> predicate)
        {
            //if (source == null)
            //{
            //    throw new ArgumentNullException(nameof(source));
            //}
            //if (predicate == null)
            //{
            //    throw new ArgumentNullException(nameof(predicate));
            //}

            var trueList = new TSource[source.Count];
            var falseList = new TSource[source.Count];
            var tInd = 0;
            var fInd = 0;

            foreach (var element in source)
            {
                if (predicate(element))
                {
                    trueList[tInd++] = element;
                }
                else
                {
                    falseList[fInd++] = element;
                }
            }

            return new PartitionResult<TSource>(trueList.Take(tInd).ToArray(), falseList.Take(fInd).ToArray());
        }
    }
}