namespace AsyncBatchAlgTest
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
					if (!executingTasks[i].IsCompleted)
						continue;

					executingTasks[i] = action(enumerator.Current);

					if (enumerator.MoveNext() is false)
						break;
				}
			}

			var t = executingTasks.Where(t => t != null).ToArray();
			await Task.WhenAll(t);
		}

		public static async IAsyncEnumerable<T> ExecuteWithResult<T>(IEnumerable<Func<Task<T>>> tasks, int count)
		{
			var executingTasks = new Task<T>[count];

			using var enumerator = tasks.GetEnumerator();

			for (var id = 0; id < count && enumerator.MoveNext(); id++)
				executingTasks[id] = enumerator.Current!();

			while (enumerator.MoveNext())
			{
				await Task.WhenAny(executingTasks);

				foreach (var index in executingTasks.GetIndexesByPredicate(z => z.IsCompleted))
				{
					yield return executingTasks[index].Result;
					executingTasks[index] = enumerator.Current!();

					if (enumerator.MoveNext() is false)
						break;
				}
			}

			var resultTasks = executingTasks.Where(t => t != null).ToArray();

			await Task.WhenAll(resultTasks);

			foreach (var task in resultTasks)
				yield return task.Result;
		}
	}
}