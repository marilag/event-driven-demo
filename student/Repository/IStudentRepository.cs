using System.Runtime.InteropServices;

namespace student {

    public interface IStudentRepository
	{
		public  Task<Student> Save(Student student);
		public  Task<IList<Student>> Get();
		public  Task<Student> Get(string studentId);



	} 
}