# Summary

Tests of algorithms for asynchronous execution of N tasks with max degree of parallelism P.

Alg example:  
```C#
public static class ParallelExecuter
{
  public static async Task ExecuteAsyncParallel<T>(this IEnumerable<T> collection, int P, Func<T, CancellationToken, ValueTask> action)
  {
    var option = new ParallelOptions { MaxDegreeOfParallelism = P };
    await Parallel.ForEachAsync(collection, option, action);
  }
}
```

# Performance Tests

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1706 (21H1/May2021Update)  
Intel Core i7-10750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores  
.NET SDK=6.0.300  
  [Host]     : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT  
  Job-FWASNW : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT  

InvocationCount=1  IterationCount=3  LaunchCount=1  
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1  

|                 Method |  N  |  P  |    Mean |    Error |   StdDev | Allocated |
|------------------------|:---:|:---:|--------:|---------:|---------:|----------:|
| EnumeratorExecutorTest |  20 |   4 | 2.771 s | 0.0522 s | 0.0029 s |     10 KB |
|    ChannelExecutorTest |  20 |   4 | 4.129 s | 0.3417 s | 0.0187 s |     13 KB |
|   ParallelExecutorTest |  20 |   4 | 4.122 s | 0.3443 s | 0.0189 s |     12 KB |
|  SemaphoreExecutorTest |  20 |   4 | 4.139 s | 0.0539 s | 0.0030 s |     14 KB |
|   DataflowExecutorTest |  20 |   4 | 4.122 s | 0.1777 s | 0.0097 s |     16 KB |
|  PartitionExecutorTest |  20 |   4 | 4.356 s | 0.2220 s | 0.0122 s |     29 KB |
|    BatchedExecutorTest |  20 |   4 | 4.895 s | 0.1678 s | 0.0092 s |     15 KB |

|                 Method |  N  |  P  |    Mean |    Error |   StdDev | Allocated |
|------------------------|:---:|:---:|--------:|---------:|---------:|----------:|
| EnumeratorExecutorTest |  20 |  20 | 1.011 s | 0.0201 s | 0.0011 s |     10 KB |
|    ChannelExecutorTest |  20 |  20 | 1.015 s | 0.1835 s | 0.0101 s |     18 KB |
|   ParallelExecutorTest |  20 |  20 | 1.013 s | 0.2920 s | 0.0160 s |     14 KB |
|  SemaphoreExecutorTest |  20 |  20 | 1.011 s | 0.1193 s | 0.0065 s |     13 KB |
|   DataflowExecutorTest |  20 |  20 | 1.014 s | 0.1373 s | 0.0075 s |     16 KB |
|  PartitionExecutorTest |  20 |  20 | 1.009 s | 0.1976 s | 0.0108 s |     66 KB |
|    BatchedExecutorTest |  20 |  20 | 1.007 s | 0.1305 s | 0.0072 s |     14 KB |

|                 Method |  N  |  P  |    Mean |    Error |   StdDev | Allocated |
|------------------------|:---:|:---:|--------:|---------:|---------:|----------:|
| EnumeratorExecutorTest |  40 |   4 | 4.867 s | 0.2373 s | 0.0130 s |     16 KB |
|    ChannelExecutorTest |  40 |   4 | 8.386 s | 0.4184 s | 0.0229 s |     22 KB |
|   ParallelExecutorTest |  40 |   4 | 8.381 s | 0.3877 s | 0.0212 s |     20 KB |
|  SemaphoreExecutorTest |  40 |   4 | 8.401 s | 0.1459 s | 0.0080 s |     25 KB |
|   DataflowExecutorTest |  40 |   4 | 8.380 s | 0.2471 s | 0.0135 s |     31 KB |
|  PartitionExecutorTest |  40 |   4 | 9.614 s | 0.2742 s | 0.0150 s |     37 KB |
|    BatchedExecutorTest |  40 |   4 | 9.637 s | 0.5066 s | 0.0278 s |     28 KB |

|                 Method |  N  |  P  |    Mean |    Error |   StdDev | Allocated |
|------------------------|:---:|:---:|--------:|---------:|---------:|----------:|
| EnumeratorExecutorTest |  40 |  20 | 1.688 s | 1.8960 s | 0.1039 s |     21 KB |
|    ChannelExecutorTest |  40 |  20 | 1.992 s | 0.1502 s | 0.0082 s |     27 KB |
|   ParallelExecutorTest |  40 |  20 | 1.992 s | 0.3013 s | 0.0165 s |     23 KB |
|  SemaphoreExecutorTest |  40 |  20 | 1.990 s | 0.0394 s | 0.0022 s |     25 KB |
|   DataflowExecutorTest |  40 |  20 | 1.994 s | 0.2812 s | 0.0154 s |     30 KB |
|  PartitionExecutorTest |  40 |  20 | 1.995 s | 0.1934 s | 0.0106 s |     74 KB |
|    BatchedExecutorTest |  40 |  20 | 2.004 s | 0.1855 s | 0.0102 s |     25 KB |

## Legends

N          : Count of tasks  
P          : Max degree of parallelism  
Mean       : Arithmetic mean of all measurements  
Error      : Half of 99.9% confidence interval  
StdDev     : Standard deviation of all measurements  
Allocated  : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)  
1 s        : 1 Second (1 sec)
