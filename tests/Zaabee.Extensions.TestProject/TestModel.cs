using System;

namespace Zaabee.Extensions.TestProject
{
    public class TestModel
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string PostTypeId { get; set; }
    }
}