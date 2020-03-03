using System;

namespace Zaabee.Extensions.TestProject
{
    public class TestModel
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }
}