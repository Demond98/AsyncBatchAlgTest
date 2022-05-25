using System.Threading.Tasks.Dataflow;
using MoreLinq.Extensions;

namespace AsyncBatchAlgTest.Executors
{
	public static class DataflowExecutor
	{
		public static async Task ExecuteDataflow<T>(this IEnumerable<T> collection, int batchCount, Func<T, Task> action)
		{
			var options = new ExecutionDataflowBlockOptions
			{
				MaxDegreeOfParallelism = batchCount
			};
			
			var actionBlock = new ActionBlock<T>(action, options);

			collection.ForEach(z => actionBlock.Post(z));
			actionBlock.Complete();

			await actionBlock.Completion;
		}
	}
}