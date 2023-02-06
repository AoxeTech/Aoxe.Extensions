namespace Zaabee.Extensions.UnitTest.Commons;

public class TestModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public DateTime Birthday { get; set; }
}