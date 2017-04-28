using System.Linq;
using BenchmarkDotNet.Attributes;
using SimpleSamples;
using SwiftHelper.Experimental;

namespace Benchmark.MinByMaxBy
{
    public class MinByMaxByBenchmarksExtended : MinByMaxByBenchmarksBase
    {
        [Params(100, 10000)]
        public override int DataSize { get; set; }

        [Benchmark]
        public SimpleUser[] MinBy_SelectWhere()
        {
            var users = Data.MinBy_SelectWhere(u => u.Age)
                .Concat(Data.MinBy_SelectWhere(u => u.Name))
                .Concat(Data.MinBy_SelectWhere(u => u.Joined))
                .Concat(Data.MinBy_SelectWhere(u => u.Score))
                .Concat(Data.MinBy_SelectWhere(u => u.Gender));

            return users.ToArray();
        }

        [Benchmark]
        public SimpleUser[] MinBy_ComparerWithArrayAndTake()
        {
            var users = Data.MinBy_ComparerWithArrayAndTake(u => u.Age)
                .Concat(Data.MinBy_ComparerWithArrayAndTake(u => u.Name))
                .Concat(Data.MinBy_ComparerWithArrayAndTake(u => u.Joined))
                .Concat(Data.MinBy_ComparerWithArrayAndTake(u => u.Score))
                .Concat(Data.MinBy_ComparerWithArrayAndTake(u => u.Gender));

            return users.ToArray();
        }

        [Benchmark(Baseline = true)]
        public SimpleUser[] MinBy_ComparerWithArrayAndCopy()
        {
            var users = Data.MinBy_ComparerWithArrayAndCopy(u => u.Age)
                .Concat(Data.MinBy_ComparerWithArrayAndCopy(u => u.Name))
                .Concat(Data.MinBy_ComparerWithArrayAndCopy(u => u.Joined))
                .Concat(Data.MinBy_ComparerWithArrayAndCopy(u => u.Score))
                .Concat(Data.MinBy_ComparerWithArrayAndCopy(u => u.Gender));

            return users.ToArray();
        }

        [Benchmark]
        public SimpleUser[] MinBy_ComparerWithList()
        {
            var users = Data.MinBy_ComparerWithList(u => u.Age)
                .Concat(Data.MinBy_ComparerWithList(u => u.Name))
                .Concat(Data.MinBy_ComparerWithList(u => u.Joined))
                .Concat(Data.MinBy_ComparerWithList(u => u.Score))
                .Concat(Data.MinBy_ComparerWithList(u => u.Gender));

            return users.ToArray();
        }
    }
}