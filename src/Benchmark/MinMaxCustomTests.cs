using BenchmarkDotNet.Attributes;

namespace Benchmark
{
    public class MinMaxCustomTests
    {
        [Params(10, 1000)]
        public int DataSize { get; set; }

        //todo
    }
}