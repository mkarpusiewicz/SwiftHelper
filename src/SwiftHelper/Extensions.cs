using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SwiftHelper
{
    public static partial class Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source == null || source.GetEnumerator().MoveNext() == false;
        }

        public static bool AllUnique<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var set = new HashSet<TSource>();

            foreach (var element in source)
            {
                if (!set.Add(element))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool AllUniqueBy<TSource, TSelector>(this IEnumerable<TSource> source, Func<TSource, TSelector> selector)
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

            foreach (var element in source)
            {
                if (!set.Add(selector(element)))
                {
                    return false;
                }
            }

            return true;
        }

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
            if (oldEnumerable == null)
            {
                throw new ArgumentNullException(nameof(oldEnumerable));
            }
            if (newEnumerable == null)
            {
                throw new ArgumentNullException(nameof(newEnumerable));
            }

            var added = newEnumerable.Except(oldEnumerable).ToArray();
            var removed = oldEnumerable.Except(newEnumerable).ToArray();

            return new SimpleCompareResult<TElement>(added, removed);
        }

        ///// <summary>
        /////     Return elements both satysfing and not the given predicate
        ///// </summary>
        ///// <typeparam name="TSource"></typeparam>
        ///// <param name="source"></param>
        ///// <param name="predicate"></param>
        ///// <returns></returns>
        public static PartitionResult<TSource> Partition<TSource>(this ICollection<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

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

        public static ICollection<TSource> MinBy<TSource, TSelector>(this ICollection<TSource> source, Func<TSource, TSelector> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            IComparer<TSelector> comparer = Comparer<TSelector>.Default;

            using (var enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    return new TSource[0]; // empty collection
                }

                var minElements = new List<TSource> {enumerator.Current};
                var minSelectorValue = selector(enumerator.Current);

                while (enumerator.MoveNext())
                {
                    var element = enumerator.Current;
                    var selectorValue = selector(element);

                    var compareResult = comparer.Compare(selectorValue, minSelectorValue);

                    if (compareResult < 0)
                    {
                        minSelectorValue = selectorValue;
                        minElements.Clear();
                        minElements.Add(element);
                    }
                    else if (compareResult == 0)
                    {
                        minElements.Add(element);
                    }
                }

                return minElements;
            }
        }

        public static ICollection<TSource> MaxBy<TSource, TSelector>(this ICollection<TSource> source, Func<TSource, TSelector> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            IComparer<TSelector> comparer = Comparer<TSelector>.Default;

            using (var enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    return new TSource[0]; // empty collection
                }

                var maxElements = new List<TSource> {enumerator.Current};
                var maxSelectorValue = selector(enumerator.Current);

                while (enumerator.MoveNext())
                {
                    var element = enumerator.Current;
                    var selectorValue = selector(element);

                    var compareResult = comparer.Compare(selectorValue, maxSelectorValue);

                    if (compareResult > 0)
                    {
                        maxSelectorValue = selectorValue;
                        maxElements.Clear();
                        maxElements.Add(element);
                    }
                    else if (compareResult == 0)
                    {
                        maxElements.Add(element);
                    }
                }

                return maxElements;
            }
        }

        public static void ForEach<TSource>(this ICollection<TSource> source, Action<TSource> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (var element in source)
            {
                action(element);
            }
        }

        public static IEnumerable<TSource> Generate<TSource>(this TSource initialValue, Func<TSource, TSource> generationAction, Func<TSource, bool> whileCondition = null)
        {
            if (generationAction == null)
            {
                throw new ArgumentNullException(nameof(generationAction));
            }

            yield return initialValue;

            if (whileCondition == null)
            {
                whileCondition = source => true; //infinite enumeration
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