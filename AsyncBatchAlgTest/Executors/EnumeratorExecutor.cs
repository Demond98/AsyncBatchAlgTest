namespace AsyncBatchAlgTest.Executors
{
	public static class EnumeratorExecutor
	{
		public static async Task ExecuteEnumerator<T>(this IEnumerable<T> collection, int batchCount, Func<T, Task> action)
		{
			var executingTasks = new Task[batchCount];

			using var enumerator = collection.GetEnumerator();

			var id = 0;
			for (; id < batchCount && enumerator.MoveNext(); id++)
				executingTasks[id] = action(enumerator.Current);

			if (id < batchCount)
			{
				await Task.WhenAll(executingTasks[..id]).ConfigureAwait(false);
				return;
			}

			while (enumerator.MoveNext())
			{
				await Task.WhenAny(executingTasks).ConfigureAwait(false);

				for (var i = 0; i < executingTasks.Length; i++)
				{
					if (executingTasks[i].IsCompleted is false)
						continue;

					executingTasks[i] = action(enumerator.Current);

					if (i == executingTasks.Length - 1 || enumerator.MoveNext() is false)
						break;
				}
			}

			await Task.WhenAll(executingTasks);
		}

		public static async IAsyncEnumerable<TOut> ExecuteWithResult<TIn, TOut>(IEnumerable<TIn> collection, int batchCount, Func<TIn, Task<TOut>> func)
		{
			var executingTasks = new Task<TOut>[batchCount];

			using var enumerator = collection.GetEnumerator();

			var id = 0;
			for (; id < batchCount && enumerator.MoveNext(); id++)
				executingTasks[id] = func(enumerator.Current);
			
			if (id < batchCount)
			{
				var tasksNeeded = executingTasks[..id];
				
				await Task.WhenAll(tasksNeeded).ConfigureAwait(false);
				
				foreach (var task in tasksNeeded)
					yield return task.Result;

				yield break;
			}

			while (enumerator.MoveNext())
			{
				await Task.WhenAny(executingTasks).ConfigureAwait(false);

				for (var i = 0; i < executingTasks.Length; i++)
				{
					if (executingTasks[i].IsCompleted is false)
						continue;

					yield return executingTasks[i].Result;
					
					executingTasks[i] = func(enumerator.Current);

					if (i == executingTasks.Length - 1 || enumerator.MoveNext() is false)
						break;
				}
			}

			await Task.WhenAll(executingTasks).ConfigureAwait(false);
			foreach (var task in executingTasks)
				yield return task.Result;
		}
	}
}