using MediatR;

namespace eventschool.enrollment
{
    public class StudentRegisteredCompleted : DomainEvent<Student>, INotification
    {
        private const string _schemaVersion = "1.0";
        public string StudentId { get; set; } = String.Empty;
        public StudentRegisteredCompleted()
        {
            EventType = nameof(StudentRegisteredCompleted);
            SchemaVersion = _schemaVersion;
        }
    }
}