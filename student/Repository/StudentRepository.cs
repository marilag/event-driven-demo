using Microsoft.Extensions.Options;

namespace student
{
    public class StudentRepository : IStudentRepository
    {
        public IOptions<StudentOptions> Options { get; }
        
        public StudentRepository(IOptions<StudentOptions> options)
        {
            Options = options;
        }

        

        public Task<IList<Student>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Student> Get(string studentId)
        {
            throw new NotImplementedException();
        }

        public Task<Student> Save(Student student)
        {
            throw new NotImplementedException();
        }
    }
}