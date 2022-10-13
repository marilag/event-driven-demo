using MediatR;

namespace eventschool
{
    public class IssueBookToStudent : IRequest<Book>
    {

        public string BookId { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;


    }
}