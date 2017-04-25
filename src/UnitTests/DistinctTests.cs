using System;
using System.Collections.Generic;
using System.Linq;
using SimpleSamples;
using SwiftHelper;
using Xunit;

namespace UnitTests
{
    public class DistinctTests
    {
        private static readonly List<SimpleUser> Users = new List<SimpleUser>
        {
            new SimpleUser {Name = "John", Gender = Gender.Male, Joined = new DateTime(2017, 1, 1), Score = 10, Age = 27},
            new SimpleUser {Name = "Amy", Gender = Gender.Female, Joined = new DateTime(2017, 2, 1), Score = 20, Age = 21},
            new SimpleUser {Name = "Kate", Gender = Gender.Female, Joined = new DateTime(2017, 3, 1), Score = 15, Age = 19}
        };

        [Fact]
        public void DistinctByTest()
        {
            var result = Users.DistinctBy(u => u.Gender).ToArray();

            Assert.Equal(2, result.Length);

            Assert.Equal("John", result[0].Name);
            Assert.Equal("Amy", result[1].Name);
        }
    }
}