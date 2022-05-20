# Summary

Tests of algorithms for asynchronous execution of N tasks with max degree of parallelism P

# Performance Tests

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1706 (21H1/May2021Update)  
Intel Core i7-10750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores  
.NET SDK=6.0.300  
  [Host]     : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT  
  Job-FWASNW : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT  

InvocationCount=1  IterationCount=3  LaunchCount=1  
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1  

|                 Method |  N  |  P  |    Mean |    Error |   StdDev | Allocated |
|----------------------- |:---:|:---:|--------:|---------:|---------:|----------:|
| EnumeratorExecuterTest |  20 |   4 | 2.765 s | 0.1960 s | 0.0107 s |     10 KB |
|    ChannelExecuterTest |  20 |   4 | 4.127 s | 0.2210 s | 0.0121 s |     13 KB |
|   ParallelExecuterTest |  20 |   4 | 4.127 s | 0.1553 s | 0.0085 s |     11 KB |
|  SemaphoreExecuterTest |  20 |   4 | 4.130 s | 0.4881 s | 0.0268 s |     14 KB |
|   DataflowExecuterTest |  20 |   4 | 4.121 s | 0.2361 s | 0.0129 s |     16 KB |
|  PartitionExecuterTest |  20 |   4 | 4.346 s | 0.3749 s | 0.0206 s |     29 KB |
| EnumeratorExecuterTest |  20 |  20 | 1.004 s | 0.1176 s | 0.0064 s |     11 KB |
|    ChannelExecuterTest |  20 |  20 | 1.014 s | 0.2296 s | 0.0126 s |     19 KB |
|   ParallelExecuterTest |  20 |  20 | 1.004 s | 0.2052 s | 0.0112 s |     13 KB |
|  SemaphoreExecuterTest |  20 |  20 | 1.007 s | 0.1326 s | 0.0073 s |     13 KB |
|   DataflowExecuterTest |  20 |  20 | 1.011 s | 0.2231 s | 0.0122 s |     16 KB |
|  PartitionExecuterTest |  20 |  20 | 1.004 s | 0.1331 s | 0.0073 s |     66 KB |
| EnumeratorExecuterTest |  40 |   4 | 4.853 s | 0.4551 s | 0.0249 s |     16 KB |
|    ChannelExecuterTest |  40 |   4 | 8.367 s | 0.3474 s | 0.0190 s |     22 KB |
|   ParallelExecuterTest |  40 |   4 | 8.376 s | 0.2648 s | 0.0145 s |     20 KB |
|  SemaphoreExecuterTest |  40 |   4 | 8.362 s | 0.1017 s | 0.0056 s |     25 KB |
|   DataflowExecuterTest |  40 |   4 | 8.375 s | 0.2242 s | 0.0123 s |     29 KB |
|  PartitionExecuterTest |  40 |   4 | 9.625 s | 0.4677 s | 0.0256 s |     37 KB |
| EnumeratorExecuterTest |  40 |  20 | 1.729 s | 1.6375 s | 0.0898 s |     21 KB |
|    ChannelExecuterTest |  40 |  20 | 1.992 s | 0.2546 s | 0.0140 s |     27 KB |
|   ParallelExecuterTest |  40 |  20 | 1.987 s | 0.1200 s | 0.0066 s |     22 KB |
|  SemaphoreExecuterTest |  40 |  20 | 1.993 s | 0.3101 s | 0.0170 s |     25 KB |
|   DataflowExecuterTest |  40 |  20 | 1.996 s | 0.2107 s | 0.0116 s |     29 KB |
|  PartitionExecuterTest |  40 |  20 | 1.998 s | 0.0751 s | 0.0041 s |     75 KB |

## Legends

RangeCount : Value of the 'RangeCount' parameter  
BatchSize  : Value of the 'BatchSize' parameter  
Mean       : Arithmetic mean of all measurements  
Error      : Half of 99.9% confidence interval  
StdDev     : Standard deviation of all measurements  
Allocated  : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)  
1 s        : 1 Second (1 sec)
