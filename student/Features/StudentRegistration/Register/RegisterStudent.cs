using MediatR;

namespace eventschool
{
public record RegisterStudent : IRequest
{ 

    public Guid Id { get; init; } = Guid.NewGuid();
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string Address { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string Program { get; init; } = default!;

}
}

