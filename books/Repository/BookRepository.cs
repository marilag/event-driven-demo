using System.Collections;
using Microsoft.Extensions.Options;

namespace eventschool
{
    public class BookRepository : IEnumerable<Book>, IBookRepository
    {
        private List<Book> Books = new List<Book>();
        private readonly IOptions<BookOptions> options;

        public BookRepository(IOptions<BookOptions> options)
        {
            SeedBooks();
            this.options = options;
        }

        private void SeedBooks()
        {
            Books.Add(new Book("b1","Fundamental of Computer Science",3));
            Books.Add(new Book("b2","Quantum Computing 1st Edition",3));
            Books.Add(new Book("b3","Machine Learnin Fundamentals",3));
            Books.Add(new Book("b4","Technology and Ethics",3));


        }

        public async Task<Book> Get(string code)
        {
            return Books.Where(b => b.BookCode.Equals(code,StringComparison.InvariantCultureIgnoreCase ))
            .FirstOrDefault() ?? throw new Exception("Book not found");
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return Books;
        }

        public async Task Save(Book book)
        {
            Books.Add(book);
        }

        public IEnumerator<Book> GetEnumerator()
        {
            return Books.GetEnumerator();
        }

        

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}