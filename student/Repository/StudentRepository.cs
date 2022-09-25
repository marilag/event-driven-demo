using System.Collections;
using Microsoft.Extensions.Options;

namespace student
{
    public class StudentRepository : IEnumerable<Student>, IStudentRepository
    {
        public IOptions<StudentOptions> Options { get; }
        
        public StudentRepository(IOptions<StudentOptions> options)
        {
            Options = options;
        }

        

        public async Task<IList<Student>> Get()
        {
            return this.ToList();
        }

        public async Task<Student> Get(string studentId)
        {
            return this.Where(s => string.Equals(s.StudentId.ToString(),studentId,
            StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
        }

        public async Task<Student> Save(Student student)
        {
            this.Append(student);
            return student;
        }

        public IEnumerator<Student> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}