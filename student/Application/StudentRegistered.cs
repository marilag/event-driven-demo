using MediatR;

namespace eventschool
{
    public class StudentRegistered : INotification
    {
        public string StudentId { get; set; }
        public string Program { get; set; }
    }
}