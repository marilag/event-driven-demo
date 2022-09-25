namespace eventschool
{
    public interface IBookRepository
    {
        public Task<Book> Get(string bookid);
        public Task Save(Book book);
        public Task<IEnumerable<Book>> GetAll();

    }
}