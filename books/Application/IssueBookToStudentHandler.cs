using MediatR;

namespace eventschool
{
    public class IssueToStudentHandler : IRequestHandler<IssueBookToStudent,Book>
    {
        private readonly IBookRepository bookRepo;
        private readonly IMediator _mediator;

        public IssueToStudentHandler(IBookRepository bookRepo, IMediator mediator)
        {
            this.bookRepo = bookRepo;
            this._mediator = mediator;
        }
        public async Task<Book> Handle(IssueBookToStudent request, CancellationToken token)
        {
            var book = await bookRepo.Get(request.BookId) ?? throw new Exception("Book not found");
            book.IssueTo(request.StudentId);
            
            await _mediator.Publish<BookIssuedToStudent>(new BookIssuedToStudent() {BookCode = book.BookCode, StudentId = request.StudentId});
            
            return book;            
        }
        
    }
}