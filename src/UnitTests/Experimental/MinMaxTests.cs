using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SimpleSamples;
using SwiftHelper.Experimental;

namespace UnitTests.Experimental
{
    [TestFixture]
    [Category("Experimental")]
    public class MinMaxTests
    {
        private static readonly List<SimpleUser> Users = new List<SimpleUser>
        {
            new SimpleUser {Name = "John", Gender = Gender.Male, Joined = new DateTime(2017, 1, 1), Score = 10, Age = 27},
            new SimpleUser {Name = "Amy", Gender = Gender.Female, Joined = new DateTime(2017, 2, 1), Score = 20, Age = 21},
            new SimpleUser {Name = "Kate", Gender = Gender.Female, Joined = new DateTime(2017, 3, 1), Score = 15, Age = 19},
            new SimpleUser {Name = "Frank", Gender = Gender.Male, Joined = new DateTime(2017, 3, 2), Score = 17, Age = 19}
        };

        [Test]
        public void MinBy_ComparerWithArrayAndTake_Test()
        {
            var result = Users.MinBy_ComparerWithArrayAndTake(u => u.Age);

            Assert.AreEqual(2, result.Count);

            var names = result.Select(s => s.Name).ToArray();

            Assert.Contains("Kate", names);
            Assert.Contains("Frank", names);
        }

        [Test]
        public void MinBy_SelectWhere_Test()
        {
            var result = Users.MinBy_SelectWhere(u => u.Age);

            Assert.AreEqual(2, result.Count);

            var names = result.Select(s => s.Name).ToArray();

            Assert.Contains("Kate", names);
            Assert.Contains("Frank", names);
        }

        [Test]
        public void MinBy_ComparerWithArrayAndCopy_Test()
        {
            var result = Users.MinBy_ComparerWithArrayAndCopy(u => u.Age);

            Assert.AreEqual(2, result.Count);

            var names = result.Select(s => s.Name).ToArray();

            Assert.Contains("Kate", names);
            Assert.Contains("Frank", names);
        }

        [Test]
        public void MinBy_ComparerWithList_Test()
        {
            var result = Users.MinBy_ComparerWithList(u => u.Age);

            Assert.AreEqual(2, result.Count);

            var names = result.Select(s => s.Name).ToArray();

            Assert.Contains("Kate", names);
            Assert.Contains("Frank", names);
        }
    }
}