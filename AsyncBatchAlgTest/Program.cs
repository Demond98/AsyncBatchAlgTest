using System.Diagnostics;

namespace AsyncBatchAlgTest
{
	class Program
	{
		static async Task Main()
		{
			BenchmarkDotNet.Running.BenchmarkRunner.Run<PerformanceTest>();			
		}
	}
}