using MediatR;

namespace eventschool
{
    public class StudentRegistered : DomainEvent<Student>, INotification
    {
        public string StudentId { get; set; } = String.Empty;
        public string Program { get; set; } = String.Empty;
    }
}