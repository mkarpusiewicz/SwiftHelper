using System;
using System.Collections.Generic;
using System.Linq;

namespace SwiftHelper.Experimental
{
    public static class MinMaxExtensions
    {
        public static ICollection<TSource> MinBy_SelectWhere<TSource, TSelector>(this ICollection<TSource> source, Func<TSource, TSelector> selector)
        {
            var minSelectorValue = source.Select(selector).Min();

            return source.Where(s => minSelectorValue.Equals(selector(s))).ToArray();
        }

        public static ICollection<TSource> MinBy_ComparerWithArrayAndTake<TSource, TSelector>(this ICollection<TSource> source, Func<TSource, TSelector> selector)
        {
            IComparer<TSelector> comparer = Comparer<TSelector>.Default;

            var minElements = new TSource[source.Count]; // allocate maximum possible size
            var elementsCount = 0;

            using (var enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    return new TSource[0]; // empty collection
                }
                var minSelectorValue = selector(enumerator.Current);

                while (enumerator.MoveNext())
                {
                    var element = enumerator.Current;
                    var selectorValue = selector(element);

                    var compareResult = comparer.Compare(selectorValue, minSelectorValue);

                    if (compareResult < 0)
                    {
                        minSelectorValue = selectorValue;
                        elementsCount = 0;
                        minElements[elementsCount++] = element;
                    }
                    else if (compareResult == 0)
                    {
                        minElements[elementsCount++] = element;
                    }
                }
            }

            return minElements.Take(elementsCount).ToArray();
        }

        public static ICollection<TSource> MinBy_ComparerWithArrayAndCopy<TSource, TSelector>(this ICollection<TSource> source, Func<TSource, TSelector> selector)
        {
            IComparer<TSelector> comparer = Comparer<TSelector>.Default;

            var minElements = new TSource[source.Count]; // allocate maximum possible size
            var elementsCount = 0;

            using (var enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    return new TSource[0]; // empty collection
                }
                var minSelectorValue = selector(enumerator.Current);

                while (enumerator.MoveNext())
                {
                    var element = enumerator.Current;
                    var selectorValue = selector(element);

                    var compareResult = comparer.Compare(selectorValue, minSelectorValue);

                    if (compareResult < 0)
                    {
                        minSelectorValue = selectorValue;
                        elementsCount = 0;
                        minElements[elementsCount++] = element;
                    }
                    else if (compareResult == 0)
                    {
                        minElements[elementsCount++] = element;
                    }
                }
            }

            var result = new TSource[elementsCount];

            Array.Copy(minElements, result, elementsCount);

            return result;
        }

        public static ICollection<TSource> MinBy_ComparerWithList<TSource, TSelector>(this ICollection<TSource> source, Func<TSource, TSelector> selector)
        {
            IComparer<TSelector> comparer = Comparer<TSelector>.Default;

            var minElements = new List<TSource>();

            using (var enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    return new TSource[0]; // empty collection
                }
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
            }

            return minElements;
        }
    }
}