using BenchmarkDotNet.Attributes;

namespace Intrinsics
{
    public class GetBenchmark
    {
        private const int ElementCount = 1_000_000_000;
        private readonly double[] testElements = new double[ElementCount];

        public GetBenchmark()
        {
            Random r = new();

            for (int i = 0; i < ElementCount; i++)
            {
                testElements[i] = r.Next();
            }
        }

        [Benchmark]
        public double MaxNet7() => testElements.Max();

        [Benchmark]
        public double MaxIntrinsics() => testElements.MaxWithIntrinsics();

        [Benchmark]
        public double MinNet7() => testElements.Min();

        [Benchmark]
        public double MinIntrinsics() => testElements.MinWithIntrinsics();

        [Benchmark]
        public double AverageNet7() => testElements.Average();

        [Benchmark]
        public double AverageIntrinsics() => testElements.AverageWithIntrinsics();

        [Benchmark]
        public double SumNet7() => testElements.Sum();

        [Benchmark]
        public double SumIntrinsics() => testElements.SumWithIntrinsics();
    }
}
