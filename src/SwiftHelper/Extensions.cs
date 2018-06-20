using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SwiftHelper
{
    public static partial class Extensions
    {
        /// <summary>
        ///     Checks if given collection is null or doesn't contain any elements.
        /// </summary>
        /// <typeparam name="TSource">Type of items in the collection</typeparam>
        /// <param name="source">Source collection to perform the checks</param>
        /// <returns>Is the collection null or if it doesn't contain any elements</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source == null || source.GetEnumerator().MoveNext() == false;
        }

        /// <summary>
        ///     Checks if all elements in the collection are unique using the default equality comparer. 
        /// </summary>
        /// <typeparam name="TSource">Type of items in the collection</typeparam>
        /// <param name="source">Source collection to perform the checks</param>
        /// <returns>If all items are unique</returns>
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

        /// <summary>
        ///     Checks if all elements in the collection are unique by checking if a given member selected by the selector
        ///     is unique using the default equality comparer. 
        /// </summary>
        /// <typeparam name="TSource">Type of items in the collection</typeparam>
        /// <typeparam name="TSelector">Type of the member selected by the selector</typeparam>
        /// <param name="source">Source collection to perform the checks</param>
        /// <param name="selector">Selector that selects the member from the item to perform uniqueness checks</param>
        /// <returns>If all items are unique using the selected member as unique identifier</returns>
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

        /// <summary>
        ///     Returns distinct elements from the collection using a given member selected by the selector
        ///     to perform distinct checks using default equality comparer.
        /// </summary>
        /// <typeparam name="TSource">Type of items in the collection</typeparam>
        /// <typeparam name="TSelector">Type of the member selected by the selector</typeparam>
        /// <param name="source">Source collection to perform the filtering</param>
        /// <param name="selector">Selector that selects the member from the item to perform distinct checks</param>
        /// <returns>Only distinct elements from the source collection determined by the selected member as unique identifier</returns>
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

        /// <summary>
        ///     Performs comparison on two collections and returs an object containing elements that were not present in the old/first collection (added)
        ///     and that were not present in the new/second collection (removed).
        /// </summary>
        /// <typeparam name="TElement">Type of items in the collection</typeparam>
        /// <param name="oldEnumerable">First collection used as baseline before changes</param>
        /// <param name="newEnumerable">Second collection after changes made to the colletion</param>
        /// <returns>Two collection with items added/removed in the second/new collection in comparison to the first/old collection</returns>
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

        /// <summary>
        ///     Return elements both satisfying and not satisfying the given predicate.
        /// </summary>
        /// <typeparam name="TSource">Type of items in the collection</typeparam>
        /// <param name="source">Source collection to perform the filtering</param>
        /// <param name="predicate">Boolean predicate on the item to determine the partition of the source collection</param>
        /// <returns>
        ///     Two collections of items, one that contains elements from the source collection that satisfy the predicate, 
        ///     and the other one that does not
        /// </returns>
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

        /// <summary>
        ///     Returns one or more elements from the source collection that have the smallest value determined by the member selected by the selector
        ///     and compared to each other using the default equality comparer.  
        /// </summary>
        /// <typeparam name="TSource">Type of items in the collection</typeparam>
        /// <typeparam name="TSelector">Type of the member selected by the selector</typeparam>
        /// <param name="source">Source collection to perform the checks</param>
        /// <param name="selector">Selector that selects the member from the item to perform the checks</param>
        /// <returns>Collection of one or more items with the minimum value of the member selected by the selector</returns>
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

        /// <summary>
        ///     Returns one or more elements from the source collection that have the biggest value determined by the member selected by the selector
        ///     and compared to each other using the default equality comparer.  
        /// </summary>
        /// <typeparam name="TSource">Type of items in the collection</typeparam>
        /// <typeparam name="TSelector">Type of the member selected by the selector</typeparam>
        /// <param name="source">Source collection to perform the checks</param>
        /// <param name="selector">Selector that selects the member from the item to perform the checks</param>
        /// <returns>Collection of one or more items with the maximum value of the member selected by the selector</returns>
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

        /// <summary>
        ///     Perform an action for each element in the collection.
        /// </summary>
        /// <typeparam name="TSource">Type of items in the collection</typeparam>
        /// <param name="source">Source collection to perform the action</param>
        /// <param name="action">The action that will be performed on every element in the source collection</param>
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

        /// <summary>
        ///     Generator function that creates an enumerable from a starting object, apply generation action after every element to the starting object 
        ///     until while condition is not met. Can be used as an infinite series generator.
        /// </summary>
        /// <typeparam name="TSource">Type of starting item</typeparam>
        /// <param name="initialValue">Initial value to be used in the generator</param>
        /// <param name="generationAction">Action taken on the current state of the generator object taken after each generation step</param>
        /// <param name="whileCondition">Condition to stop the generation (if null or always true the generator will generate an infinite series)</param>
        /// <returns>Enumerable object to iterate over to get items in the generator series</returns>
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