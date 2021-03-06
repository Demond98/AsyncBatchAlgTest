namespace AsyncBatchAlgTest.Executors
{
	public static class ParallelExecutor
	{
		public static async Task ExecuteAsyncParallel<T>(this IEnumerable<T> collection, int batchCount, Func<T, CancellationToken, ValueTask> action)
		{
			var option = new ParallelOptions { MaxDegreeOfParallelism = batchCount };
			await Parallel.ForEachAsync(collection, option, action);
		}
	}
}