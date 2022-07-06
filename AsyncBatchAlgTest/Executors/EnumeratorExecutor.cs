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

			var flag = true;
			while (flag)
			{
				await Task.WhenAny(executingTasks).ConfigureAwait(false);

				for (var i = 0; i < batchCount; i++)
				{
					if (executingTasks[i].IsCompleted is false)
						continue;

					if (enumerator.MoveNext() is false)
					{
						flag = false;
						break;
					}

					executingTasks[i] = action(enumerator.Current);
				}
			}

			await Task.WhenAll(executingTasks).ConfigureAwait(false);
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

			var flag = true;
			while (flag)
			{
				await Task.WhenAny(executingTasks).ConfigureAwait(false);

				for (var i = 0; i < batchCount; i++)
				{
					if (executingTasks[i].IsCompleted is false)
						continue;

					if (enumerator.MoveNext() is false)
					{
						flag = false;
						break;
					}

					executingTasks[i] = func(enumerator.Current);
					yield return executingTasks[i].Result;
				}
			}

			await Task.WhenAll(executingTasks).ConfigureAwait(false);
			foreach (var task in executingTasks)
				yield return task.Result;
		}
	}
}