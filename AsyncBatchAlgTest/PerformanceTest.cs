using AsyncBatchAlgTest.Executors;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace AsyncBatchAlgTest
{
	[MemoryDiagnoser, SimpleJob(RunStrategy.ColdStart, launchCount: 1, warmupCount: 1, targetCount: 3, invocationCount: 1)]
	public class PerformanceTest
	{
		private const int MinDelayTime = 500;
		private const int MaxDelayTime = 1000;
		private int[] _range = Array.Empty<int>();

		[Params(20, 40)]
		public int RangeCount;
		[Params(4, 20)]
		public int BatchSize;

		private static async ValueTask GetDelayTask(int time)
		{
			await Task.Delay(time);
			await ValueTask.CompletedTask;
		}

		[GlobalSetup]
		public void GlobalSetup()
		{
			var random = new Random(0);
			_range = Enumerable.Range(0, RangeCount).Select(_ => random.Next(MinDelayTime, MaxDelayTime)).ToArray();
		}

		[Benchmark]
		public async Task EnumeratorExecutorTest()
		{
			Func<int, Task> getDelay = static async z => await GetDelayTask(z);
			var actions = _range.Select(z => getDelay.Partial(z));
			await actions.ExecuteEnumerator(BatchSize);
		}

		[Benchmark]
		public async Task ChannelExecutorTest()
		{
			await _range.ExecuteChannel(BatchSize, static async a => await GetDelayTask(a));
		}

		[Benchmark]
		public async Task ParallelExecutorTest()
		{
			await _range.ExecuteAsyncParallel(BatchSize, static async (a, _) => await GetDelayTask(a));
		}

		[Benchmark]
		public async Task SemaphoreExecutorTest()
		{
			await _range.ExecuteSemaphore(BatchSize, static async a => await GetDelayTask(a));
		}

		[Benchmark]
		public async Task DataflowExecutorTest()
		{
			await _range.ExecuteDataflow(BatchSize, static async a => await GetDelayTask(a));
		}

		[Benchmark]
		public async Task PartitionExecutorTest()
		{
			await _range.ExecutePartition(BatchSize, static async a => await GetDelayTask(a));
		}
	}
}