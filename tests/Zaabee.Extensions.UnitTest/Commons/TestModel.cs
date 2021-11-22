namespace Zaabee.Extensions.UnitTest.Commons;

public class TestModel
{
    public Guid Id { get; } = Guid.NewGuid();
    public string? Name { get; set; }
    public DateTime Birthday { get; set; }
}