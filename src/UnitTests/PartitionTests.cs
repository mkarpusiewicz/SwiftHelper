using System;
using System.Collections.Generic;
using System.Linq;
using SimpleSamples;
using SwiftHelper;
using Xunit;

namespace UnitTests
{
    [Trait("Category", "Production")]
    public class PartitionTests
    {
        public delegate SwiftHelper.Extensions.PartitionResult<SimpleUser> PartitionDelegate(Func<SimpleUser, bool> predicate);

        public static readonly List<SimpleUser> Users = new List<SimpleUser>
        {
            new SimpleUser {Name = "John", Gender = Gender.Male, Joined = new DateTime(2017, 1, 1), Score = 10, Age = 27, Rating = new Rating(3)},
            new SimpleUser {Name = "Amy", Gender = Gender.Female, Joined = new DateTime(2017, 2, 1), Score = 20, Age = 21, Rating = new Rating(2)},
            new SimpleUser {Name = "Kate", Gender = Gender.Female, Joined = new DateTime(2017, 3, 1), Score = 15, Age = 19, Rating = new Rating(1)},
            new SimpleUser {Name = "Frank", Gender = Gender.Male, Joined = new DateTime(2017, 3, 2), Score = 17, Age = 19, Rating = new Rating(4)}
        };

        [Fact]
        public void PartitionByAgeTests()
        {
            var result = Users.Partition(u => u.Age > 20);

            Assert.Equal(2, result.True.Count);
            Assert.Equal(2, result.False.Count);

            var namesTrue = result.True.Select(s => s.Name).ToArray();
            var namesFalse = result.False.Select(s => s.Name).ToArray();

            Assert.Contains("John", namesTrue);
            Assert.Contains("Amy", namesTrue);
            Assert.Contains("Kate", namesFalse);
            Assert.Contains("Frank", namesFalse);
        }

        [Fact]
        public void PartitionByGenderTests()
        {
            var result = Users.Partition(u => u.Gender == Gender.Male);

            Assert.Equal(2, result.True.Count);
            Assert.Equal(2, result.False.Count);

            var namesTrue = result.True.Select(s => s.Name).ToArray();
            var namesFalse = result.False.Select(s => s.Name).ToArray();

            Assert.Contains("John", namesTrue);
            Assert.Contains("Frank", namesTrue);
            Assert.Contains("Kate", namesFalse);
            Assert.Contains("Amy", namesFalse);
        }
    }
}