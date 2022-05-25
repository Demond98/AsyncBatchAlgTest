﻿namespace AsyncBatchAlgTest.Executors
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
				await Task.WhenAll(executingTasks);
				return;
			}

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
				await Task.WhenAll(executingTasks);
				foreach (var task in executingTasks)
					yield return task.Result;

				yield break;
			}

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

			await Task.WhenAll(executingTasks);
			foreach (var task in executingTasks)
				yield return task.Result;
		}
	}
}