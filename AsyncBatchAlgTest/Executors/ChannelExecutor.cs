using System.Threading.Channels;

namespace AsyncBatchAlgTest.Executors
{
	public static class ChannelExecutor
	{
		public static async Task ExecuteChannel<T>(this IEnumerable<T> collection, int batchCount, Func<T, Task> action)
		{
			var myChannel = Channel.CreateUnbounded<T>(new UnboundedChannelOptions { SingleWriter = true });
			var reader = myChannel.Reader;
			var writer = myChannel.Writer;

			foreach (var value in collection)
				writer.TryWrite(value);
				
			writer.Complete();

			var tasks = Enumerable.Range(0, batchCount).Select(async _ =>
			{
				while (await reader.WaitToReadAsync())
				{
					var value = await reader.ReadAsync();
					await action(value);
				}
			}).ToArray();

			await Task.WhenAll(tasks);
		}
	}
}