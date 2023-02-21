using MediatR;

namespace eventschool
{
public class RegisterStudent : IRequest<Student>
{ 
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Program { get; set; } = string.Empty;

}
}

