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
| EnumeratorExecuterTest |  20 |   4 | 2.779 s | 0.3262 s | 0.0179 s |     10 KB |
|    ChannelExecuterTest |  20 |   4 | 4.119 s | 0.4687 s | 0.0257 s |     13 KB |
|   ParallelExecuterTest |  20 |   4 | 4.127 s | 0.4819 s | 0.0264 s |     12 KB |
|  SemaphoreExecuterTest |  20 |   4 | 4.123 s | 0.3273 s | 0.0179 s |     14 KB |
| EnumeratorExecuterTest |  20 |  20 | 1.003 s | 0.1200 s | 0.0066 s |     11 KB |
|    ChannelExecuterTest |  20 |  20 | 1.013 s | 0.0629 s | 0.0034 s |     18 KB |
|   ParallelExecuterTest |  20 |  20 | 1.009 s | 0.1885 s | 0.0103 s |     13 KB |
|  SemaphoreExecuterTest |  20 |  20 | 4.119 s | 0.1296 s | 0.0071 s |     14 KB |
| EnumeratorExecuterTest |  40 |   4 | 4.857 s | 0.1411 s | 0.0077 s |     16 KB |
|    ChannelExecuterTest |  40 |   4 | 8.381 s | 0.5453 s | 0.0299 s |     22 KB |
|   ParallelExecuterTest |  40 |   4 | 8.366 s | 0.1903 s | 0.0104 s |     20 KB |
|  SemaphoreExecuterTest |  40 |   4 | 8.377 s | 0.2574 s | 0.0141 s |     25 KB |
| EnumeratorExecuterTest |  40 |  20 | 1.812 s | 0.0709 s | 0.0039 s |     21 KB |
|    ChannelExecuterTest |  40 |  20 | 1.995 s | 0.1879 s | 0.0103 s |     27 KB |
|   ParallelExecuterTest |  40 |  20 | 1.998 s | 0.0869 s | 0.0048 s |     22 KB |
|  SemaphoreExecuterTest |  40 |  20 | 8.371 s | 0.2219 s | 0.0122 s |     25 KB |

## Legends

RangeCount : Value of the 'RangeCount' parameter  
BatchSize  : Value of the 'BatchSize' parameter  
Mean       : Arithmetic mean of all measurements  
Error      : Half of 99.9% confidence interval  
StdDev     : Standard deviation of all measurements  
Allocated  : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)  
1 s        : 1 Second (1 sec)
