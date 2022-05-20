# Summary

Tests of algorithms for asynchronous execution of N tasks with max degree of parallelism P

# Performance Tests

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1706 (21H1/May2021Update)  
Intel Core i7-10750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores  
.NET SDK=6.0.300  
  [Host]     : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT  
  Job-IUACQQ : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT  

InvocationCount=1  IterationCount=3  LaunchCount=1  
RunStrategy=ColdStart  UnrollFactor=1  WarmupCount=1  

|                 Method |  N  |  P  |    Mean |    Error |   StdDev | Allocated |
|----------------------- |:---:|:---:|-------- |---------:|---------:|----------:|
|    ChannelExecuterTest | 20  |  4  | 4.112 s | 0.2064 s | 0.0113 s |     14 KB |
| EnumeratorExecuterTest | 20  |  4  | 2.774 s | 0.2173 s | 0.0119 s |     10 KB |
|   ParallelExecuterTest | 20  |  4  | 4.121 s | 0.0906 s | 0.0050 s |     12 KB |
|    ChannelExecuterTest | 20  | 20  | 1.012 s | 0.1501 s | 0.0082 s |     21 KB |
| EnumeratorExecuterTest | 20  | 20  | 1.006 s | 0.0135 s | 0.0007 s |     11 KB |
|   ParallelExecuterTest | 20  | 20  | 1.010 s | 0.1587 s | 0.0087 s |     13 KB |
|    ChannelExecuterTest | 40  |  4  | 8.373 s | 0.1941 s | 0.0106 s |     23 KB |
| EnumeratorExecuterTest | 40  |  4  | 4.859 s | 0.2522 s | 0.0138 s |     16 KB |
|   ParallelExecuterTest | 40  |  4  | 8.377 s | 0.5342 s | 0.0293 s |     22 KB |
|    ChannelExecuterTest | 40  | 20  | 1.999 s | 0.4116 s | 0.0226 s |     29 KB |
| EnumeratorExecuterTest | 40  | 20  | 1.689 s | 1.9015 s | 0.1042 s |     21 KB |
|   ParallelExecuterTest | 40  | 20  | 1.993 s | 0.2159 s | 0.0118 s |     22 KB |

## Legends

RangeCount : Value of the 'RangeCount' parameter  
BatchSize  : Value of the 'BatchSize' parameter  
Mean       : Arithmetic mean of all measurements  
Error      : Half of 99.9% confidence interval  
StdDev     : Standard deviation of all measurements  
Allocated  : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)  
1 s        : 1 Second (1 sec)
