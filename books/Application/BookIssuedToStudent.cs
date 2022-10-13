using MediatR;

namespace eventschool
{
    public class BookIssuedToStudent : INotification
    {
        public string StudentId { get; set; } = string.Empty;
        public string BookCode { get; set; } = string.Empty;
    }
}