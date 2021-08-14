using System;
using System.Linq;
using System.Collections.Generic;

public static class Extensions
{
	public static void ForEach<T>(this IList<T> list, Action<T, int> callback, int? forceIterations = null) where T : class
	{
		int count = forceIterations ?? list.Count;
		for (int i = 0; i < count; ++i)
		{
			callback.Invoke(i < list.Count ? list[i] : null, i);
		}
	}

	public static void SelectRemove<T>(this IList<T> list, Func<T, bool> predicate, Action onRemove = null) where T : class
	{
		for (int i = list.Count - 1; i >= 0; --i)
		{
			if (predicate.Invoke(list[i]))
			{
				list.RemoveAt(i);
				onRemove?.Invoke();
			}
		}
	}
}
