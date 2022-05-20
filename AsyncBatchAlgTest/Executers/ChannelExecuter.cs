using System.Threading.Channels;

namespace AsyncBatchAlgTest.Executers
{
	public static class ChannelExecutor
	{
		public static async Task ExecuteChannel<T>(this IEnumerable<T> collection, int batchCount, Func<T, Task> action)
		{
			var myChannel = Channel.CreateUnbounded<T>(new UnboundedChannelOptions() { SingleWriter = true });
			var reader = myChannel.Reader;
			var writer = myChannel.Writer;

			foreach (var value in collection)
				writer.TryWrite(value);
			writer.Complete();

			var tasks = new byte[batchCount].Select(async _ =>
			{
				await foreach (var value in reader.ReadAllAsync())
					await action(value);
			}).ToArray();

			await Task.WhenAll(tasks);
		}
	}
}