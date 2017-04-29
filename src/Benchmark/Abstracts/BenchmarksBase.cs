using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Bogus;
using SimpleSamples;

namespace Benchmark.Abstracts
{
    public abstract class BenchmarksBase
    {
        public abstract int DataSize { get; set; }

        protected List<SimpleUser> Data { get; set; }

        [Setup]
        public void Setup()
        {
            Data = GetTestData();
        }

        private List<SimpleUser> GetTestData()
        {
            var ratings = Enumerable.Range(1, 20).Select(r => new Rating(r));

            var faker = new Faker<SimpleUser>()
                .StrictMode(true)
                .RuleFor(u => u.Name, f => f.Name.FindName())
                .RuleFor(u => u.Age, f => f.Random.Number(18, 60))
                .RuleFor(u => u.Joined, f => f.Date.Past())
                .RuleFor(u => u.Score, f => (decimal) f.Random.Double())
                .RuleFor(u => u.Gender, f => f.PickRandom<Gender>())
                .RuleFor(u => u.Rating, f => f.PickRandom(ratings));

            return faker.Generate(DataSize).ToList();
        }
    }
}