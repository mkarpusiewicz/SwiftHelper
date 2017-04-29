using System.Linq;
using SimpleSamples;
using SwiftHelper;
using Xunit;

namespace UnitTests
{
    [Trait("Category", "Production")]
    public class AllUniqueTests
    {
        [Fact]
        public void AllUniqueByFalse()
        {
            var data = Enumerable.Range(1, 100).Concat(new[] {50, 1, 100}).Select(i => new SimpleUser {Name = $"Name{i}", Age = i});

            var result = data.AllUniqueBy(u => u.Age);

            Assert.False(result);
        }

        [Fact]
        public void AllUniqueByTrue()
        {
            var data = Enumerable.Range(1, 100).Select(i => new SimpleUser {Name = $"Name{i}", Age = i});

            var result = data.AllUniqueBy(u => u.Age);

            Assert.True(result);
        }

        [Fact]
        public void AllUniqueFalse()
        {
            var data = Enumerable.Range(1, 100).Concat(new[] {50, 1, 100});

            var result = data.AllUnique();

            Assert.False(result);
        }

        [Fact]
        public void AllUniqueTrue()
        {
            var data = Enumerable.Range(1, 100);

            var result = data.AllUnique();

            Assert.True(result);
        }
    }
}