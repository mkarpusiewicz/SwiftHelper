using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Bogus;
using SimpleSamples;
using SwiftHelper.Experimental;

namespace Benchmark
{
    public class MinMaxTests
    {
        [Params(10, 100, 1000, 10000)]
        public int DataSize { get; set; }

        private List<SimpleUser> Data { get; set; }

        [Setup]
        public void Setup()
        {
            Data = GetTestData();
        }

        private List<SimpleUser> GetTestData()
        {
            var faker = new Faker<SimpleUser>()
                .StrictMode(true)
                .RuleFor(u => u.Name, f => f.Name.FindName())
                .RuleFor(u => u.Age, f => f.Random.Number(18, 60))
                .RuleFor(u => u.Joined, f => f.Date.Past())
                .RuleFor(u => u.Score, f => (decimal) f.Random.Double())
                .RuleFor(u => u.Gender, f => f.PickRandom<Gender>());

            return faker.Generate(DataSize).ToList();
        }

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