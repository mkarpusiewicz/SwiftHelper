using System;
using System.Collections.Generic;
using System.Linq;
using SimpleSamples;
using Xunit;

namespace UnitTests
{
    [Trait("Category", "Production")]
    public class CompareTests
    {
        private static readonly List<SimpleUser> OldUsers = new List<SimpleUser>
        {
            new SimpleUser {Name = "John", Gender = Gender.Male, Joined = new DateTime(2017, 1, 1), Score = 10, Age = 27},
            new SimpleUser {Name = "Frank", Gender = Gender.Male, Joined = new DateTime(2017, 1, 1), Score = 4, Age = 35},
            new SimpleUser {Name = "Amy", Gender = Gender.Female, Joined = new DateTime(2017, 2, 1), Score = 20, Age = 21}
        };

        private static readonly List<SimpleUser> NewUsers = new List<SimpleUser>
        {
            new SimpleUser {Name = "John", Gender = Gender.Male, Joined = new DateTime(2017, 1, 1), Score = 10, Age = 27},
            new SimpleUser {Name = "Amy", Gender = Gender.Female, Joined = new DateTime(2017, 2, 1), Score = 20, Age = 21},
            new SimpleUser {Name = "Kate", Gender = Gender.Female, Joined = new DateTime(2017, 3, 1), Score = 15, Age = 19}
        };

        [Fact]
        public void CompareUsersTest()
        {
            var result = SwiftHelper.Extensions.Compare(OldUsers, NewUsers);

            Assert.Equal(1, result.Added.Count);
            Assert.Contains(NewUsers.Single(u => u.Name == "Kate"), result.Added);

            Assert.Equal(1, result.Removed.Count);
            Assert.Contains(OldUsers.Single(u => u.Name == "Frank"), result.Removed);
        }

        [Fact]
        public void CompareIntegersTest()
        {
            var oldList = new List<int> { 1, 2, 3 };
            var newList = new List<int> { 1, 2, 4, 5 };

            var result = SwiftHelper.Extensions.Compare(oldList, newList);

            Assert.Equal(2, result.Added.Count);
            Assert.Contains(4, result.Added);
            Assert.Contains(5, result.Added);

            Assert.Equal(1, result.Removed.Count);
            Assert.Contains(3, result.Removed);
        }
    }
}