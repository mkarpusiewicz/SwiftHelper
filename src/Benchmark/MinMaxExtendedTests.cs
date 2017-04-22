using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Bogus;
using SimpleSamples;
using SwiftHelper.Experimental;

namespace Benchmark
{
    public class MinMaxExtendedTests
    {
        [Params(10, 1000)]
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
            var youngestUsers = Data.MinBy_SelectWhere(u => u.Age)
                .Concat(Data.MinBy_SelectWhere(u => u.Name))
                .Concat(Data.MinBy_SelectWhere(u => u.Joined))
                .Concat(Data.MinBy_SelectWhere(u => u.Score))
                .Concat(Data.MinBy_SelectWhere(u => u.Gender));

            return youngestUsers.ToArray();
        }

        [Benchmark]
        public SimpleUser[] MinBy_ComparerWithArrayAndTake()
        {
            var youngestUsers = Data.MinBy_ComparerWithArrayAndTake(u => u.Age)
                .Concat(Data.MinBy_ComparerWithArrayAndTake(u => u.Name))
                .Concat(Data.MinBy_ComparerWithArrayAndTake(u => u.Joined))
                .Concat(Data.MinBy_ComparerWithArrayAndTake(u => u.Score))
                .Concat(Data.MinBy_ComparerWithArrayAndTake(u => u.Gender));

            return youngestUsers.ToArray();
        }

        [Benchmark(Baseline = true)]
        public SimpleUser[] MinBy_ComparerWithArrayAndCopy()
        {
            var youngestUsers = Data.MinBy_ComparerWithArrayAndCopy(u => u.Age)
                .Concat(Data.MinBy_ComparerWithArrayAndCopy(u => u.Name))
                .Concat(Data.MinBy_ComparerWithArrayAndCopy(u => u.Joined))
                .Concat(Data.MinBy_ComparerWithArrayAndCopy(u => u.Score))
                .Concat(Data.MinBy_ComparerWithArrayAndCopy(u => u.Gender));

            return youngestUsers.ToArray();
        }

        [Benchmark]
        public SimpleUser[] MinBy_ComparerWithList()
        {
            var youngestUsers = Data.MinBy_ComparerWithList(u => u.Age)
                .Concat(Data.MinBy_ComparerWithList(u => u.Name))
                .Concat(Data.MinBy_ComparerWithList(u => u.Joined))
                .Concat(Data.MinBy_ComparerWithList(u => u.Score))
                .Concat(Data.MinBy_ComparerWithList(u => u.Gender));

            return youngestUsers.ToArray();
        }
    }
}