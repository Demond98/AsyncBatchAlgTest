using System.Threading.Channels;

namespace AsyncBatchAlgTest
{
	public static class ChannelExecutor
	{
		public static async Task ExecuteChannel<T>(this IEnumerable<T> collection, int batchCount, Func<T, Task> action)
		{
			var myChannel = Channel.CreateUnbounded<T>();
			var reader = myChannel!.Reader;
			var writer = myChannel!.Writer;

			foreach (var value in collection)
				writer.TryWrite(value);

			writer.Complete();

			async Task Read()
			{
				await foreach (var value in reader.ReadAllAsync())
					await action(value);
			}

			var tasks = Enumerable.Range(0, batchCount).Select(async _ => await Read()).ToArray();
			await Task.WhenAll(tasks);
		}
	}
}