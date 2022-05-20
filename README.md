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
| EnumeratorExecuterTest |  20 |   4 | 2.770 s | 0.2966 s | 0.0163 s |     10 KB |
|    ChannelExecuterTest |  20 |   4 | 4.119 s | 0.3079 s | 0.0169 s |     13 KB |
|   ParallelExecuterTest |  20 |   4 | 4.118 s | 0.0708 s | 0.0039 s |     12 KB |
|  SemaphoreExecuterTest |  20 |   4 | 4.121 s | 0.0682 s | 0.0037 s |     14 KB |
|   DataflowExecuterTest |  20 |   4 | 4.124 s | 0.2634 s | 0.0144 s |     16 KB |
| EnumeratorExecuterTest |  20 |  20 | 1.012 s | 0.0652 s | 0.0036 s |     11 KB |
|    ChannelExecuterTest |  20 |  20 | 1.016 s | 0.1433 s | 0.0079 s |     19 KB |
|   ParallelExecuterTest |  20 |  20 | 1.011 s | 0.2319 s | 0.0127 s |     13 KB |
|  SemaphoreExecuterTest |  20 |  20 | 1.007 s | 0.0478 s | 0.0026 s |     13 KB |
|   DataflowExecuterTest |  20 |  20 | 1.006 s | 0.1943 s | 0.0107 s |     16 KB |
| EnumeratorExecuterTest |  40 |   4 | 4.852 s | 0.2637 s | 0.0145 s |     16 KB |
|    ChannelExecuterTest |  40 |   4 | 8.354 s | 0.5772 s | 0.0316 s |     22 KB |
|   ParallelExecuterTest |  40 |   4 | 8.381 s | 0.2888 s | 0.0158 s |     20 KB |
|  SemaphoreExecuterTest |  40 |   4 | 8.367 s | 0.2317 s | 0.0127 s |     25 KB |
|   DataflowExecuterTest |  40 |   4 | 8.378 s | 0.3968 s | 0.0218 s |     30 KB |
| EnumeratorExecuterTest |  40 |  20 | 1.688 s | 2.0521 s | 0.1125 s |     23 KB |
|    ChannelExecuterTest |  40 |  20 | 1.993 s | 0.2262 s | 0.0124 s |     27 KB |
|   ParallelExecuterTest |  40 |  20 | 1.990 s | 0.1572 s | 0.0086 s |     23 KB |
|  SemaphoreExecuterTest |  40 |  20 | 1.992 s | 0.0364 s | 0.0020 s |     25 KB |
|   DataflowExecuterTest |  40 |  20 | 1.998 s | 0.0712 s | 0.0039 s |     29 KB |

## Legends

RangeCount : Value of the 'RangeCount' parameter  
BatchSize  : Value of the 'BatchSize' parameter  
Mean       : Arithmetic mean of all measurements  
Error      : Half of 99.9% confidence interval  
StdDev     : Standard deviation of all measurements  
Allocated  : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)  
1 s        : 1 Second (1 sec)
