using MoreLinq;

namespace AsyncBatchAlgTest.Executors;

public static class SimpleBatchExecutor
{
    public static async Task ExecuteBatched<T>(this IEnumerable<T> source, int batchCount, Func<T, Task> action)
    {
        foreach (var block in source.Batch(batchCount))
        {
            var tasks = block.Select(async z => await action(z));
            await Task.WhenAll(tasks);
        }
    }
    
    public static async IAsyncEnumerable<TOut> ExecuteBatchedWithResult<TIn, TOut>(this IEnumerable<TIn> source, int batchCount, Func<TIn, Task<TOut>> func)
    {
        foreach (var block in source.Batch(batchCount))
        {
            var tasks = block.Select(async z => await func(z)).ToArray();
            await Task.WhenAll(tasks);

            foreach (var task in tasks)
                yield return task.Result;
        }
    }
}