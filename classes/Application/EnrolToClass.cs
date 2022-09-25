using MediatR;

namespace eventschool
{
    public class EnrolToClass : IRequest<Class>

    {
        public string ClassId { get; set; } = String.Empty;
        public string StudentId { get; set; } = String.Empty; 
        
    }
}