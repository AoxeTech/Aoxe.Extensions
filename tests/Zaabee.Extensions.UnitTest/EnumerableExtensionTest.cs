using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Zaabee.Extensions.UnitTest
{
    public class EnumerableExtensionTest
    {
        [Fact]
        public void ToListTest()
        {
            var i = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 0};
            var even = i.ToList(p => p % 2 == 0);
            Assert.True(even.All(p => p % 2 == 0));
            IEnumerable<int> nullEnumerable = null;
            Assert.Throws<ArgumentNullException>(() => nullEnumerable.ToList(p => p % 2 == 0));
        }

        [Fact]
        public void ForEachTest()
        {
            var testModels = new List<TestModel>
            {
                new TestModel {Name = "Alice"},
                new TestModel {Name = "Alice"},
                new TestModel {Name = "Alice"},
                new TestModel {Name = "Alice"},
                new TestModel {Name = "Alice"}
            };
            var enumerable = testModels.AsEnumerable();
            enumerable.ForEach(p => p.Name = "Bob");
            Assert.True(enumerable.All(p => p.Name == "Bob"));
            enumerable.ForEach(null);
            IEnumerable<TestModel> nullEnumerable = null;
            Assert.Throws<ArgumentNullException>(() => nullEnumerable.ForEach(p => p.Name = "Bob"));
        }

        [Fact]
        public void ForEachLazyTest()
        {
            var testModels = new List<TestModel>
            {
                new TestModel {Name = "Alice", Birthday = new DateTime(2000, 1, 1)},
                new TestModel {Name = "Alice", Birthday = new DateTime(2000, 1, 1)},
                new TestModel {Name = "Alice", Birthday = new DateTime(2000, 1, 1)},
                new TestModel {Name = "Alice", Birthday = new DateTime(2000, 1, 1)},
                new TestModel {Name = "Alice", Birthday = new DateTime(2000, 1, 1)}
            };
            var enumerable = testModels.AsEnumerable();
            var result = enumerable.ForEachLazy(p => p.Name = "Bob")
                    .ForEachLazy(p => p.Birthday = new DateTime(2001, 1, 1))
                    .ToList();
            Assert.True(result.All(p => p.Name == "Bob" && p.Birthday == new DateTime(2001, 1, 1)));
            enumerable.ForEachLazy(null);
            IEnumerable<TestModel> nullEnumerable = null;
            Assert.Throws<ArgumentNullException>(() => nullEnumerable.ForEachLazy(p => p.Name = "Bob"));
        }
    }
}