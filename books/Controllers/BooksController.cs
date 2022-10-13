using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eventschool.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
  

    private readonly ILogger<BooksController> _logger;
    private readonly IMediator _mediator;

    public IBookRepository _booksRepo { get; }

    public BooksController(ILogger<BooksController> logger, IMediator mediator, IBookRepository booksRepo)
    {
        _logger = logger;
        _mediator = mediator;
        _booksRepo = booksRepo;
    }

    [HttpPost]
    [Route("{bookid}/issued")]
    public async Task<Book> IssueToStudent(string bookid, [FromBody] string studentid)
    {
        var result = await _mediator.Send<Book>(new IssueBookToStudent() {BookId = bookid, StudentId = studentid});
        return result;
    } 

    [HttpGet]
    public async Task<IEnumerable<Book>> GetBooks()
    {
        return (await _booksRepo.GetAll());
    } 

    [HttpGet]
    [Route("{code}")]
    public async Task<Book> GetClassById(string code)
    {
        return (await _booksRepo.Get(code));
    } 

    
}
