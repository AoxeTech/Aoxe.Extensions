using System;

namespace Zaabee.Extensions.UnitTest
{
    public class TestModel
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }
}