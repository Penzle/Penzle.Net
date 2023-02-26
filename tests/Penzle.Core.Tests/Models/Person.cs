namespace Penzle.Core.Tests.Models;

public sealed class Person
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Age { get; set; }
    public Address? Address { get; set; }
}
