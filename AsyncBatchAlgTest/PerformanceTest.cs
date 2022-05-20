using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace AsyncBatchAlgTest
{
	[MemoryDiagnoser, SimpleJob(RunStrategy.ColdStart, launchCount: 1, warmupCount: 1, targetCount: 3, invocationCount: 1)]
	public class PerformanceTest
	{
		private const int MinDelayTime = 500;
		private const int MaxDelayTime = 1000;
		private int[] _range;

		[Params(20, 40)]
		public int RangeCount;
		[Params(4, 20)]
		public int BatchSize;

		public static async ValueTask GetDelayTask(int time)
		{
			await Task.Delay(time);
			await ValueTask.CompletedTask;
		}

		[GlobalSetup]
		public void GlobalSetup()
		{
			var random = new Random(0);
			_range = Enumerable.Range(0, RangeCount).Select(a => random.Next(MinDelayTime, MaxDelayTime)).ToArray();
		}

		[Benchmark]
		public async Task EnumeratorExecuterTest()
		{
			await _range.ExecuteEnumerator(BatchSize, static async a => await GetDelayTask(a));
		}

		[Benchmark]
		public async Task ChannelExecuterTest()
		{
			await _range.ExecuteChannel(BatchSize, static async a => await GetDelayTask(a));
		}

		[Benchmark]
		public async Task ParallelExecuterTest()
		{
			await _range.ExecuteAsyncParallel(BatchSize, static async (a, c) => await GetDelayTask(a));
		}

		[Benchmark]
		public async Task SemaphoreExecuterTest()
		{
			await _range.ExecuteSemaphore(4, static async a => await GetDelayTask(a));
		}
	}
}
