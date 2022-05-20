using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncBatchAlgTest.Executers
{
	public static class PartitionExecuter
	{
        public static async Task ExecutePartition<T>(this IEnumerable<T> source, int dop, Func<T, Task> body)
        {
			await Task.WhenAll(Partitioner.Create(source)
				.GetPartitions(dop)
				.Select(async a =>
				{
					while (a.MoveNext())
						await body(a.Current);
				}));
        }
    }
}
