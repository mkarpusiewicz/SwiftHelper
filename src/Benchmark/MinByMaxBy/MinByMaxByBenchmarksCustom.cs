using System.Linq;
using BenchmarkDotNet.Attributes;
using SimpleSamples;
using SwiftHelper.Experimental;

namespace Benchmark.MinByMaxBy
{
    public class MinByMaxByBenchmarksCustom : MinByMaxByBenchmarksBase
    {
        [Params(100, 10000)]
        public override int DataSize { get; set; }

        [Benchmark]
        public SimpleUser[] MinBy_SelectWhere()
        {
            var lowest = Data.MinBy_SelectWhere(u => u.Rating).ToArray();

            return lowest;
        }

        [Benchmark]
        public SimpleUser[] MinBy_ComparerWithArrayAndTake()
        {
            var lowest = Data.MinBy_ComparerWithArrayAndTake(u => u.Rating).ToArray();

            return lowest;
        }

        [Benchmark(Baseline = true)]
        public SimpleUser[] MinBy_ComparerWithArrayAndCopy()
        {
            var lowest = Data.MinBy_ComparerWithArrayAndCopy(u => u.Rating).ToArray();

            return lowest;
        }

        [Benchmark]
        public SimpleUser[] MinBy_ComparerWithList()
        {
            var lowest = Data.MinBy_ComparerWithList(u => u.Rating).ToArray();

            return lowest;
        }
    }
}