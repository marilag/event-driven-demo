
public record Student
{
    public Guid StudentId { get; init; } = default!;
    public string FirstName { get; init;} = default!;
    public string LastName { get; init;} = default!;
    public string Address { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string Program { get; init; } = default!;
    public DateTime RegistrationDate { get; init; }   
    
}
