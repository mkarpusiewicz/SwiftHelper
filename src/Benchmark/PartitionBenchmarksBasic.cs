using System.Linq;
using Benchmark.Abstracts;
using BenchmarkDotNet.Attributes;
using SimpleSamples;
using SwiftHelper.Experimental;

namespace Benchmark
{
    public class PartitionBenchmarksBasic : BenchmarksBase
    {
        [Params(10, 100, 1000, 10000)]
        public override int DataSize { get; set; }

        [Benchmark(Baseline = true)]
        public SimpleUser[] PartitionLists()
        {
            var partitionResult = Data.PartitionLists(u => u.Age > 30);

            return partitionResult.True.Concat(partitionResult.False).ToArray();
        }

        [Benchmark]
        public SimpleUser[] PartitionListsPresized()
        {
            var partitionResult = Data.PartitionListsPresized(u => u.Age > 30);

            return partitionResult.True.Concat(partitionResult.False).ToArray();
        }

        [Benchmark]
        public SimpleUser[] PartitionArray()
        {
            var partitionResult = Data.PartitionArray(u => u.Age > 30);

            return partitionResult.True.Concat(partitionResult.False).ToArray();
        }
    }
}