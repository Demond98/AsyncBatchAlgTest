using AsyncBatchAlgTest.Executors;

namespace AsyncBatchAlgTest
{
	internal static class Program
	{
		internal static async Task TestAsync()
		{
			static async Task GetDelayTask(int time)
			{
				Console.WriteLine($"start {time}");
				await Task.Delay(time);
				Console.WriteLine($"	done {time}");
			}

			var random = new Random(0);
			var range = Enumerable.Range(0, 100).Select(a => random.Next(500, 1000)).ToArray();

			await range.ExecuteChannel(4, GetDelayTask);
		}

		internal static void Main()
		{
			//await TestAsync();

			BenchmarkDotNet.Running.BenchmarkRunner.Run<PerformanceTest>();
		}
	}
}