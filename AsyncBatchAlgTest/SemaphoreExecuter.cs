namespace AsyncBatchAlgTest
{
	public static class SemaphoreExecuter
	{
		private static async Task AwaitFunc<T>(Func<T, Task> action, T value, Semaphore semaphore)
		{
			await action(value);
			semaphore.Release();
		}

		public static async Task ExecuteSemaphore<T>(this IEnumerable<T> collection, int batchCount, Func<T, Task> action)
		{
			var task = new List<Task>(10);
			var semaphore = new Semaphore(batchCount, batchCount);

			foreach (var value in collection)
			{
				semaphore.WaitOne();
				task.Add(AwaitFunc(action, value, semaphore));
			}

			await Task.WhenAll(task);
		}
	}
}