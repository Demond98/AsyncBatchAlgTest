using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace AsyncBatchAlgTest.Executers
{
	public static class DataflowExecuter
	{
		public static async Task ExecuteDataflow<T>(this IEnumerable<T> collection, int batchCount, Func<T, Task> action)
		{
			var actionBlock = new ActionBlock<T>(action, new ExecutionDataflowBlockOptions
			{
				MaxDegreeOfParallelism = batchCount
			});

			foreach (var item in collection)
				actionBlock.Post(item);
			actionBlock.Complete();

			await actionBlock.Completion;
		}
	}
}