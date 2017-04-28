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
    public class MinByMaxByExperimentalTests
    {
        public delegate ICollection<SimpleUser> MinByDelegate(Func<SimpleUser, object> action);

        public static readonly List<SimpleUser> Users = new List<SimpleUser>
        {
            new SimpleUser {Name = "John", Gender = Gender.Male, Joined = new DateTime(2017, 1, 1), Score = 10, Age = 27, Rating = new Rating(3)},
            new SimpleUser {Name = "Amy", Gender = Gender.Female, Joined = new DateTime(2017, 2, 1), Score = 20, Age = 21, Rating = new Rating(2)},
            new SimpleUser {Name = "Kate", Gender = Gender.Female, Joined = new DateTime(2017, 3, 1), Score = 15, Age = 19, Rating = new Rating(1)},
            new SimpleUser {Name = "Frank", Gender = Gender.Male, Joined = new DateTime(2017, 3, 2), Score = 17, Age = 19, Rating = new Rating(4)}
        };

        public static readonly List<object[]> Actions = new List<object[]>
        {
            new object[] {new ActionWrapper<MinByDelegate>(selector => Users.MinBy_ComparerWithArrayAndTake(selector))},
            new object[] {new ActionWrapper<MinByDelegate>(selector => Users.MinBy_SelectWhere(selector))},
            new object[] {new ActionWrapper<MinByDelegate>(selector => Users.MinBy_ComparerWithArrayAndCopy(selector))},
            new object[] {new ActionWrapper<MinByDelegate>(selector => Users.MinBy_ComparerWithList(selector))}
        };

        [Theory]
        [MemberData(nameof(Actions))]
        public void MinByAgeTests(ActionWrapper<MinByDelegate> action)
        {
            var result = action.Do(u => u.Age);

            Assert.Equal(2, result.Count);

            var names = result.Select(s => s.Name).ToArray();

            Assert.Contains("Kate", names);
            Assert.Contains("Frank", names);
        }

        [Theory]
        [MemberData(nameof(Actions))]
        public void MinByRatingTests(ActionWrapper<MinByDelegate> action)
        {
            var result = action.Do(u => u.Rating);

            Assert.Equal(1, result.Count);
            Assert.Equal("Kate", result.First().Name);
        }
    }
}