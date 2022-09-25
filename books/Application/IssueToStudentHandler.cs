using MediatR;

namespace eventschool
{
    public class IssueToStudentHandler : IRequestHandler<IssueToStudent,Book>
    {
        private readonly IBookRepository bookRepo;

        public IssueToStudentHandler(IBookRepository bookRepo)
        {
            this.bookRepo = bookRepo;
        }
        public async Task<Book> Handle(IssueToStudent request, CancellationToken token)
        {
            var book = await bookRepo.Get(request.BookId) ?? throw new Exception("Book not found");
            book.IssueTo(request.StudentId);
            
            return book;            
        }
        
    }
}