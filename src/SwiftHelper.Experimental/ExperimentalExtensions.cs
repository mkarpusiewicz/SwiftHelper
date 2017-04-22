using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SwiftHelper.Experimental
{
    public static class ExperimentalExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmptyWithAny<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmptyAnyNull<T>(this IEnumerable<T> source)
        {
            return !source?.Any() ?? false;
        }

        //fastest
        public static IEnumerable<TSource> DistinctBySetWhere<TSource, TSelector>(this IEnumerable<TSource> source, Func<TSource, TSelector> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            var set = new Set<TSelector>();

            return source.Where(s => set.Add(selector(s)));
        }

        //todo
        public static IEnumerable<TSource> DistinctByHash<TSource, TSelector>(this IEnumerable<TSource> source, Func<TSource, TSelector> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            // use dictionary hash implementation with equality comparer

            return null;
        }

        public static IEnumerable<TSource> DistinctBySetForEach<TSource, TSelector>(this IEnumerable<TSource> source, Func<TSource, TSelector> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            var set = new Set<TSelector>();

            foreach (var element in source)
            {
                if (set.Add(selector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}