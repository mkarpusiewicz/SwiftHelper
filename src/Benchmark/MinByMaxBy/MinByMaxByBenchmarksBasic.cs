using System.Linq;
using BenchmarkDotNet.Attributes;
using SimpleSamples;
using SwiftHelper.Experimental;

namespace Benchmark.MinByMaxBy
{
    public class MinByMaxByBenchmarksBasic : MinByMaxByBenchmarksBase
    {
        [Params(10, 100, 1000, 10000)]
        public override int DataSize { get; set; }

        [Benchmark]
        public SimpleUser[] MinBy_SelectWhere()
        {
            var youngestUsers = Data.MinBy_SelectWhere(u => u.Age).ToArray();

            return youngestUsers;
        }

        [Benchmark]
        public SimpleUser[] MinBy_ComparerWithArrayAndTake()
        {
            var youngestUsers = Data.MinBy_ComparerWithArrayAndTake(u => u.Age).ToArray();

            return youngestUsers;
        }

        [Benchmark(Baseline = true)]
        public SimpleUser[] MinBy_ComparerWithArrayAndCopy()
        {
            var youngestUsers = Data.MinBy_ComparerWithArrayAndCopy(u => u.Age).ToArray();

            return youngestUsers;
        }

        [Benchmark]
        public SimpleUser[] MinBy_ComparerWithList()
        {
            var youngestUsers = Data.MinBy_ComparerWithList(u => u.Age).ToArray();

            return youngestUsers;
        }
    }
}