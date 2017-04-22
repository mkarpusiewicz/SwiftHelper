using System;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace Benchmark
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var config = ManualConfig.Create(DefaultConfig.Instance);

            BenchmarkRunner.Run<DistinctTests>(config);
            Console.ReadKey();

            BenchmarkRunner.Run<ExtensionTests>(config);
            Console.ReadKey();
        }
    }
}