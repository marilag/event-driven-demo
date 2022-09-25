namespace eventschool
{
    public class Book
    {
        public Guid Id { get; private set; } = System.Guid.NewGuid();

        public string BookCode { get; set; } = String.Empty;
        public string Title { get; private set; } = String.Empty;
        public int Copies { get; set; }

        private readonly HashSet<string> _issuedTo = new HashSet<string>();

        public IReadOnlyCollection<string> IssuedTo => _issuedTo;        

        public Book(string code, string title, int copies)
        {
            BookCode = code;
            Title = title; 
            Copies = copies;
            
        }
        public void IssueTo(string studenId)
        {
            if (_issuedTo.Count() == Copies)            
                throw new Exception("No more copies left");
            
            _issuedTo.Add(studenId);
        }

        public void Return(string studentId)
        {
            _issuedTo.Remove(studentId);
        }

    }
}