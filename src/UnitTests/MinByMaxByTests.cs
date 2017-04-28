using System;
using System.Collections.Generic;
using System.Linq;
using SimpleSamples;
using SwiftHelper;
using Xunit;

namespace UnitTests
{
    [Trait("Category", "Production")]
    public class MinByMaxByTests
    {
        public static readonly List<SimpleUser> Users = new List<SimpleUser>
        {
            new SimpleUser {Name = "John", Gender = Gender.Male, Joined = new DateTime(2017, 1, 1), Score = 10, Age = 27, Rating = new Rating(3)},
            new SimpleUser {Name = "Amy", Gender = Gender.Female, Joined = new DateTime(2017, 2, 1), Score = 20, Age = 21, Rating = new Rating(2)},
            new SimpleUser {Name = "Erin", Gender = Gender.Male, Joined = new DateTime(2017, 2, 2), Score = 22, Age = 27, Rating = new Rating(2)},
            new SimpleUser {Name = "Kate", Gender = Gender.Female, Joined = new DateTime(2017, 3, 1), Score = 15, Age = 19, Rating = new Rating(1)},
            new SimpleUser {Name = "Frank", Gender = Gender.Male, Joined = new DateTime(2017, 3, 2), Score = 17, Age = 19, Rating = new Rating(4)}
        };

        [Fact]
        public void MaxByAgeTests()
        {
            var result = Users.MaxBy(u => u.Age);

            Assert.Equal(2, result.Count);

            var names = result.Select(s => s.Name).ToArray();

            Assert.Contains("John", names);
            Assert.Contains("Erin", names);
        }

        [Fact]
        public void MaxByRatingTests()
        {
            var result = Users.MaxBy(u => u.Rating);

            Assert.Equal(1, result.Count);
            Assert.Equal("Frank", result.First().Name);
        }

        [Fact]
        public void MinByAgeTests()
        {
            var result = Users.MinBy(u => u.Age);

            Assert.Equal(2, result.Count);

            var names = result.Select(s => s.Name).ToArray();

            Assert.Contains("Kate", names);
            Assert.Contains("Frank", names);
        }

        [Fact]
        public void MinByRatingTests()
        {
            var result = Users.MinBy(u => u.Rating);

            Assert.Equal(1, result.Count);
            Assert.Equal("Kate", result.First().Name);
        }
    }
}