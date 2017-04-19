using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SwiftHelper
{
    public static partial class Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || source.GetEnumerator().MoveNext() == false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TSource> DistinctBy<TSource, TSelector>(this IEnumerable<TSource> source, Func<TSource, TSelector> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            var set = new HashSet<TSelector>();

            return source.Where(s => set.Add(selector(s)));
        }

        public static SimpleCompareResult<TElement> Compare<TElement>(ICollection<TElement> oldEnumerable, ICollection<TElement> newEnumerable)
        {
            var added = newEnumerable.Except(oldEnumerable).ToArray();
            var removed = oldEnumerable.Except(newEnumerable).ToArray();

            return new SimpleCompareResult<TElement>(added, removed);
        }

        /// <summary>
        ///     Return elements both satysfing and not the given predicate
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static PartitionResult<TSource> Partition<TSource>(this ICollection<TSource> source, Func<TSource, bool> predicate)
        {
            return new PartitionResult<TSource>(null, null);
        }

        public static IEnumerable<TSource> MinBy<TSource, TKey>(this ICollection<TSource> source, Func<TSource, TKey> keySelector)
        {
            return null;
        }

        public static IEnumerable<TSource> MaxBy<TSource, TKey>(this ICollection<TSource> source, Func<TSource, TKey> keySelector)
        {
            return null;
        }

        public static void ForEach<TSource>(this ICollection<TSource> source, Action<TSource> action)
        {
            foreach (var element in source)
            {
                action(element);
            }
        }

        public static IEnumerable<TSource> Generate<TSource>(this TSource initialValue, Func<TSource, TSource> generationAction, Func<TSource, bool> whileCondition = null)
        {
            yield return initialValue;

            if (whileCondition == null)
            {
                whileCondition = source => true;
            }

            var currentValue = initialValue;

            while (whileCondition(currentValue))
            {
                currentValue = generationAction(currentValue);

                yield return currentValue;
            }
        }
    }
}