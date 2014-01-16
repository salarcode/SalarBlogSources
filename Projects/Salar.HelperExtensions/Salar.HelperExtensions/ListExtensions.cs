using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace System
{
	public static class ListExtensions
	{
		/// <summary>
		/// Gets the value associated with the specified key, returns default value if key is not present!
		/// </summary>
		public static TVal Get<TKey, TVal>(this IDictionary<TKey, TVal> dictionary, TKey key)
		{
			TVal result;
			dictionary.TryGetValue(key, out result);
			return result;
		}

		public static string Join(this StringCollection coll, string seperator)
		{
			var result = new StringBuilder();
			for (int i = 0; i < coll.Count; i++)
			{
				result.Append(coll[i]);
				if (i + 1 < coll.Count)
				{
					result.Append(seperator);
				}
			}
			return result.ToString();
		}

		/// <summary>
		/// Performs the specified action on each element of the IEnumerable.
		/// </summary>
		public static void ForEachAction<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			if (action == null || enumerable == null)
			{
				throw new ArgumentNullException();
			}

			foreach (var item in enumerable)
			{
				action.Invoke(item);
			}
		}

		/// <summary>
		/// Returns the first element of a sequence, or a default value if the
		/// sequence contains no elements.
		/// </summary>
		/// <typeparam name="T">The type of the elements of
		/// <paramref name="source"/>.</typeparam>
		/// <param name="source">The <see cref="IEnumerable&lt;T&gt;"/> to return
		/// the first element of.</param>
		/// <param name="default">The default value to return if the sequence contains
		/// no elements.</param>
		/// <returns><paramref name="default"/> if <paramref name="source"/> is empty;
		/// otherwise, the first element in <paramref name="source"/>.</returns>
		public static T FirstOrDefault<T>(this IEnumerable<T> source, T @default)
		{
			if (source == null)
				throw new ArgumentNullException("source");
			using (var e = source.GetEnumerator())
			{
				if (!e.MoveNext())
					return @default;
				return e.Current;
			}
		}

		/// <summary>
		/// Returns the first element of a sequence, or a default value if the sequence
		/// contains no elements.
		/// </summary>
		/// <typeparam name="T">The type of the elements of
		/// <paramref name="source"/>.</typeparam>
		/// <param name="source">The <see cref="IEnumerable&lt;T&gt;"/> to return
		/// the first element of.</param>
		/// <param name="predicate">A function to test each element for a
		/// condition.</param>
		/// <param name="default">The default value to return if the sequence contains
		/// no elements.</param>
		/// <returns><paramref name="default"/> if <paramref name="source"/> is empty
		/// or if no element passes the test specified by <paramref name="predicate"/>;
		/// otherwise, the first element in <paramref name="source"/> that passes
		/// the test specified by <paramref name="predicate"/>.</returns>
		public static T FirstOrDefault<T>(this IEnumerable<T> source,
			Func<T, bool> predicate, T @default)
		{
			if (source == null)
				throw new ArgumentNullException("source");
			if (predicate == null)
				throw new ArgumentNullException("predicate");
			using (var e = source.GetEnumerator())
			{
				while (true)
				{
					if (!e.MoveNext())
						return @default;
					if (predicate(e.Current))
						return e.Current;
				}
			}
		}

		/// <summary>
		/// Removes the all the elements that match the conditions defined by the specified predicate.
		/// </summary>
		public static void RemoveFromList<TSource>(this IList<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null || predicate == null)
			{
				throw new ArgumentNullException();
			}
			for (int i = source.Count - 1; i >= 0; i--)
				if (predicate.Invoke(source[i]))
				{
					source.RemoveAt(i);
				}
		}

		/// <summary>
		/// Adds range of items to the collection 
		/// </summary>
		public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> enumerable)
		{
			foreach (var item in enumerable)
			{
				collection.Add(item);
			}
		}

		/// <summary>
		/// Convertion to ObservableCollection
		/// </summary>
		public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new ObservableCollection<T>(source);
		}

		/// <summary>
		/// For a large list (say 100000 records) this method is one or two seconds faster
		/// </summary>
		public static TSource FirstOrDefaultFast<TSource>(this IList<TSource> list, Func<TSource, bool> predicate)
		{
			if (list == null || (predicate == null))
			{
				throw new ArgumentNullException();
			}
			for (int i = 0; i < list.Count; i++)
			{
				var item = list[i];
				if (predicate(item))
				{
					return item;
				}
			}
			return default(TSource);
		}

		/// <summary>
		/// Index of an item according to the specified predicate
		/// </summary>
		public static int FirstIndexOf<T>(this IList<T> list, Func<T, bool> predicate)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (predicate.Invoke(list[i]))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>
		/// Removes first item found
		/// </summary>
		public static void RemoveFirst<T>(this IList<T> list, Func<T, bool> predicate)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (predicate.Invoke(list[i]))
				{
					list.RemoveAt(i);
					return;
				}
			}
		}

		/// <summary>
		/// Removes last item found
		/// </summary>
		public static void RemoveLast<T>(this IList<T> list, Func<T, bool> predicate)
		{
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (predicate.Invoke(list[i]))
				{
					list.RemoveAt(i);
					return;
				}
			}
		}

		/// <summary>
		/// Finds an item which has minimum value based on the specified function. For a list.
		/// </summary>
		public static T MinValue<T>(this IList<T> list, Func<T, int> function)
		{
			if (function == null || list == null)
			{
				throw new ArgumentNullException();
			}

			int minValue = int.MaxValue;
			T result = default(T);
			for (int i = 0; i < list.Count; i++)
			{
				var item = list[i];
				var val = function.Invoke(item);
				if (val < minValue)
				{
					result = item;
					minValue = val;
				}
			}
			return result;
		}

		/// <summary>
		/// Finds an item which has minimum value based on the specified function. For enumerables. 
		/// </summary>
		public static T MinValue<T>(this IEnumerable<T> enumerable, Func<T, int> function)
		{
			if (function == null || enumerable == null)
			{
				throw new ArgumentNullException();
			}

			int minValue = int.MaxValue;
			T result = default(T);
			foreach (var item in enumerable)
			{
				var val = function.Invoke(item);
				if (val < minValue)
				{
					result = item;
					minValue = val;
				}
			}
			return result;
		}

		/// <summary>
		/// Finds an item which has maximum value based on the specified function. For enumerables. 
		/// </summary>
		public static T MaxValue<T>(this IList<T> list, Func<T, int> function)
		{
			if (function == null || list == null)
			{
				throw new ArgumentNullException();
			}

			int maxValue = int.MinValue;
			T result = default(T);
			for (int i = 0; i < list.Count; i++)
			{
				var item = list[i];

				var val = function.Invoke(item);
				if (val > maxValue)
				{
					result = item;
					maxValue = val;
				}
			}
			return result;
		}

		/// <summary>
		/// Finds an item which has maximum value based on the specified function. For enumerables. 
		/// </summary>
		public static T MaxValue<T>(this IEnumerable<T> enumerable, Func<T, int> function)
		{
			if (function == null || enumerable == null)
			{
				throw new ArgumentNullException();
			}

			int maxValue = int.MinValue;
			T result = default(T);
			foreach (var item in enumerable)
			{
				var val = function.Invoke(item);
				if (val > maxValue)
				{
					result = item;
					maxValue = val;
				}
			}
			return result;
		}

	}
}
