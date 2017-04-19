using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SimpleSamples;
using SwiftHelper;

namespace UnitTests
{
    [TestFixture]
    public class DistinctTests
    {
        private static readonly List<SimpleUser> Users = new List<SimpleUser>
        {
            new SimpleUser {Name = "John", Gender = Gender.Male, Joined = new DateTime(2017, 1, 1), Score = 10, Age = 27},
            new SimpleUser {Name = "Amy", Gender = Gender.Female, Joined = new DateTime(2017, 2, 1), Score = 20, Age = 21},
            new SimpleUser {Name = "Kate", Gender = Gender.Female, Joined = new DateTime(2017, 3, 1), Score = 15, Age = 19}
        };

        [Test]
        public void DistinctByTest()
        {
            var result = Users.DistinctBy(u => u.Gender).ToArray();

            Assert.AreEqual(2, result.Length);

            Assert.AreEqual("John", result[0].Name);
            Assert.AreEqual("Amy", result[1].Name);
        }
    }
}