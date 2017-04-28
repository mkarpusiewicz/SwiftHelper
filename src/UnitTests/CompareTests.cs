using System;
using System.Collections.Generic;
using SimpleSamples;
using Xunit;

namespace UnitTests
{
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
        }
    }
}