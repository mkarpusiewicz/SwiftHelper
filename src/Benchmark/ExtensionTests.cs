using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Bogus;
using SimpleSamples;
using SwiftHelper;
using SwiftHelper.Experimental;

namespace Benchmark
{
    public class ExtensionTests
    {
        private const int DataSize = 10000;

        private List<SimpleUser> Data { get; set; }
        private List<SimpleUser> Data2 { get; set; }
        private List<SimpleUser> Data3 { get; set; }

        [Setup]
        public void Setup()
        {
            Data = GetTestData();
            Data2 = null;
            Data3 = new List<SimpleUser>();
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
        public bool IsNullOrEmpty()
        {
            var empty1 = Data.IsNullOrEmpty();
            var empty2 = Data2.IsNullOrEmpty();
            var empty3 = Data3.IsNullOrEmpty();

            return empty1 && empty2 && empty3;
        }

        [Benchmark]
        public bool IsNullOrEmptyWithAny()
        {
            var empty1 = Data.IsNullOrEmptyWithAny();
            var empty2 = Data2.IsNullOrEmptyWithAny();
            var empty3 = Data3.IsNullOrEmptyWithAny();

            return empty1 && empty2 && empty3;
        }

        [Benchmark]
        public bool IsNullOrEmptyAnyNull()
        {
            var empty1 = Data.IsNullOrEmptyAnyNull();
            var empty2 = Data2.IsNullOrEmptyAnyNull();
            var empty3 = Data3.IsNullOrEmptyAnyNull();

            return empty1 && empty2 && empty3;
        }
    }
}