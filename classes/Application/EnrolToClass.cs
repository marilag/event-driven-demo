using MediatR;

namespace eventschool
{
    public class EnrolToClass : IRequest

    {
        public string ProgramId { get; set; } = String.Empty;
        public string StudentId { get; set; } = String.Empty; 
        
    }
}