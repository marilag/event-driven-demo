using System.Collections;
using Microsoft.Extensions.Options;

namespace student
{
    public class StudentRepository : IEnumerable<Student>, IStudentRepository
    {
        public IOptions<StudentOptions> Options { get; }
        public List<Student> StudentList; 

        public StudentRepository(IOptions<StudentOptions> options)
        {
            Options = options;
            StudentList = new List<Student>();
        }

        

        public async Task<IEnumerable<Student>> Get()
        {
            return StudentList;
        }

        public async Task<Student?> Get(string studentId)
        {
            return StudentList.Where(s => string.Equals(s.StudentId.ToString(),studentId,
            StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
        }

        public async Task<Student> Save(Student student)
        {
            StudentList.Add(student);
            return student;
        }

        public IEnumerator<Student> GetEnumerator()
        {
            return StudentList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}