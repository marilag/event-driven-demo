using System.Text.Json.Serialization;
using MediatR;

namespace eventschool
{
    public class StudentRegistered : DomainEvent<Student>, INotification
    {
        private const string _schemaVersion = "1.0";
        public string StudentId { get; set; } = String.Empty;
        public string Program { get; set; } = String.Empty;

        public StudentRegistered()
        {
            EventType = nameof(StudentRegistered);
            SchemaVersion = _schemaVersion;
        }

        [JsonConstructor]
        public StudentRegistered(Student data)
        {
            Data = data;
        }
    }
}