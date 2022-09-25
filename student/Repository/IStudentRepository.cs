using System.Runtime.InteropServices;

namespace student {

    public interface IStudentRepository
	{
		public  Task<Student> Save(Student student);
		public  Task<IEnumerable<Student>> Get();
		public  Task<Student> Get(string studentId);



	} 
}