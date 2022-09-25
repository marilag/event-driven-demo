using MediatR;

namespace eventschool
{
    public class IssueToStudent : IRequest<Book>
    {

        public string BookId { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;


    }
}