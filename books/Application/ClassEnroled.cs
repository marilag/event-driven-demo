using MediatR;

namespace eventschool
{
    public class ClassEnroledNotification : INotification
    {
        public string ClassCode { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
    }
}