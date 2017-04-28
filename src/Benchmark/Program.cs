using System;
using Benchmark.MinByMaxBy;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace Benchmark
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var config = ManualConfig.Create(DefaultConfig.Instance);

            //BenchmarkRunner.Run<DistinctTests>(config);
            //BenchmarkRunner.Run<ExtensionTests>(config);

            BenchmarkRunner.Run<MinByMaxByBenchmarksBasic>(config);
            BenchmarkRunner.Run<MinByMaxByBenchmarksExtended>(config);
            BenchmarkRunner.Run<MinByMaxByBenchmarksCustom>(config);

            Console.ReadKey();
        }
    }
}