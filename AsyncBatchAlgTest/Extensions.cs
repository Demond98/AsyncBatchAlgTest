namespace AsyncBatchAlgTest
{
	public static class Extensions
	{
		public static Lazy<Task> ToLazy(this Func<Task> func)
		{
			return new Lazy<Task>(func);
		}

		public static Func<TResult> Partial<T, TResult>(this Func<T, TResult> func, T arg)
		{
			return () => func(arg);
		}

		public static IEnumerable<int> GetIndexesByPredicate<T>(this T[] array, Predicate<T> predicate)
		{
			for (var i = 0; i < array.Length; i++)
				if (predicate(array[i]))
					yield return i;
		}

		public static Func<T, bool> Negate<T>(this Func<T, bool> predicate)
		{
			return x => !predicate(x);
		}
	}
}