using Intrinsics;
using BenchmarkDotNet.Running;

var summary = BenchmarkRunner.Run<GetBenchmark>();

Console.WriteLine(summary);