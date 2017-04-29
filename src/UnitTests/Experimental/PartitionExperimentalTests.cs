using System;
using System.Collections.Generic;
using System.Linq;
using SimpleSamples;
using SwiftHelper.Experimental;
using UnitTests.Extensions;
using Xunit;

namespace UnitTests.Experimental
{
    [Trait("Category", "Experimental")]
    public class PartitionExperimentalTests
    {
        public delegate PartitionResult<SimpleUser> PartitionDelegate(Func<SimpleUser, bool> predicate);

        public static readonly List<SimpleUser> Users = new List<SimpleUser>
        {
            new SimpleUser {Name = "John", Gender = Gender.Male, Joined = new DateTime(2017, 1, 1), Score = 10, Age = 27, Rating = new Rating(3)},
            new SimpleUser {Name = "Amy", Gender = Gender.Female, Joined = new DateTime(2017, 2, 1), Score = 20, Age = 21, Rating = new Rating(2)},
            new SimpleUser {Name = "Kate", Gender = Gender.Female, Joined = new DateTime(2017, 3, 1), Score = 15, Age = 19, Rating = new Rating(1)},
            new SimpleUser {Name = "Frank", Gender = Gender.Male, Joined = new DateTime(2017, 3, 2), Score = 17, Age = 19, Rating = new Rating(4)}
        };

        public static readonly List<object[]> Actions = new List<object[]>
        {
            new object[] {new ActionWrapper<PartitionDelegate>(predicate => Users.PartitionLists(predicate))},
            new object[] {new ActionWrapper<PartitionDelegate>(predicate => Users.PartitionListsPresized(predicate))},
            new object[] {new ActionWrapper<PartitionDelegate>(predicate => Users.PartitionArray(predicate))}
        };

        [Theory]
        [MemberData(nameof(Actions))]
        public void PartitionByAgeTests(ActionWrapper<PartitionDelegate> action)
        {
            var result = action.Do(u => u.Age > 20);

            Assert.Equal(2, result.True.Count);
            Assert.Equal(2, result.False.Count);

            var namesTrue = result.True.Select(s => s.Name).ToArray();
            var namesFalse = result.False.Select(s => s.Name).ToArray();

            Assert.Contains("John", namesTrue);
            Assert.Contains("Amy", namesTrue);
            Assert.Contains("Kate", namesFalse);
            Assert.Contains("Frank", namesFalse);
        }

        [Theory]
        [MemberData(nameof(Actions))]
        public void PartitionByGenderTests(ActionWrapper<PartitionDelegate> action)
        {
            var result = action.Do(u => u.Gender == Gender.Male);

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