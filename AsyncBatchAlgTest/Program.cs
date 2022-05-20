namespace AsyncBatchAlgTest
{
	class Program
	{
		static void Main()
		{
			BenchmarkDotNet.Running.BenchmarkRunner.Run<PerformanceTest>();
		}
	}
}