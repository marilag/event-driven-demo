using MediatR;

namespace eventschool
{
    public record EnrolToClass : IRequest

    {
        public string ProgramId { get; init; } = String.Empty;
        public string StudentId { get; init; } = String.Empty; 
        
    }
}