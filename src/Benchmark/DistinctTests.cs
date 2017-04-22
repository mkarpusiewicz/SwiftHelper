using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Bogus;
using SimpleSamples;
using SwiftHelper;
using SwiftHelper.Experimental;

namespace Benchmark
{
    public class DistinctTests
    {
        private const int DataSize = 1000;

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
        public SimpleUser[] DistinctBy()
        {
            var distinctByAge = Data.DistinctBy(u => u.Age).ToArray();
            var distinctByGender = Data.DistinctBy(u => u.Gender).ToArray();
            var distinctByName = Data.DistinctBy(u => u.Name).ToArray();

            return distinctByAge.Concat(distinctByGender).Concat(distinctByName).ToArray();
        }

        [Benchmark]
        public SimpleUser[] DistinctBySetWhere()
        {
            var distinctByAge = Data.DistinctBySetWhere(u => u.Age).ToArray();
            var distinctByGender = Data.DistinctBySetWhere(u => u.Gender).ToArray();
            var distinctByName = Data.DistinctBySetWhere(u => u.Name).ToArray();

            return distinctByAge.Concat(distinctByGender).Concat(distinctByName).ToArray();
        }


        [Benchmark]
        public SimpleUser[] DistinctBySetForEach()
        {
            var distinctByAge = Data.DistinctBySetForEach(u => u.Age).ToArray();
            var distinctByGender = Data.DistinctBySetForEach(u => u.Gender).ToArray();
            var distinctByName = Data.DistinctBySetForEach(u => u.Name).ToArray();

            return distinctByAge.Concat(distinctByGender).Concat(distinctByName).ToArray();
        }
    }
}