namespace AsyncBatchAlgTest.Executors
{
	public static class EnumeratorExecutor
	{
		public static async Task ExecuteEnumerator<T>(this IEnumerable<T> collection, int batchCount, Func<T, Task> action)
		{
			var executingTasks = new Task[batchCount];

			using var enumerator = collection.GetEnumerator();

			for (var id = 0; id < batchCount && enumerator.MoveNext(); id++)
				executingTasks[id] = action(enumerator.Current);

			while (enumerator.MoveNext())
			{
				await Task.WhenAny(executingTasks);

				for (var i = 0; i < executingTasks.Length; i++)
				{
					if (executingTasks[i].IsCompleted is false)
						continue;

					executingTasks[i] = action(enumerator.Current);

					if (enumerator.MoveNext() is false)
						break;
				}
			}

			var t = executingTasks.Where(t => t is not null).ToArray();
			await Task.WhenAll(t);
		}

		public static async IAsyncEnumerable<TOut> ExecuteWithResult<TIn, TOut>(IEnumerable<TIn> collection, int count, Func<TIn, Task<TOut>> func)
		{
			var executingTasks = new Task<TOut>[count];

			using var enumerator = collection.GetEnumerator();

			for (var id = 0; id < count && enumerator.MoveNext(); id++)
				executingTasks[id] = func(enumerator.Current);

			while (enumerator.MoveNext())
			{
				await Task.WhenAny(executingTasks);

				for (var i = 0; i < executingTasks.Length; i++)
				{
					if (executingTasks[i].IsCompleted is false)
						continue;

					yield return executingTasks[i].Result;
					
					executingTasks[i] = func(enumerator.Current);

					if (enumerator.MoveNext() is false)
						break;
				}
			}

			var resultTasks = executingTasks.Where(t => t is not null).ToArray();

			await Task.WhenAll(resultTasks);

			foreach (var task in resultTasks)
				yield return task.Result;
		}
	}
}