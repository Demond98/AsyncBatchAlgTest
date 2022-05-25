using System.Collections.Concurrent;

namespace AsyncBatchAlgTest.Executors
{
	public static class PartitionExecutor
	{
        public static async Task ExecutePartition<T>(this IEnumerable<T> source, int dop, Func<T, Task> body)
        {
			await Task.WhenAll(Partitioner.Create(source)
				.GetPartitions(dop)
				.Select(async a =>
				{
					while (a.MoveNext())
						await body(a.Current);
				}));
        }
    }
}
