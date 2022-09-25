namespace student
{
    public class EnrolStudentHandler
    {
        public EnrolStudentHandler(IStudentRepository repo)
        {
            Repo = repo;
        }

        public IStudentRepository Repo { get; }

        public async Task<Student> Handle(EnrolStudent request, CancellationToken cancellationToken)
        {            
            var newStudent = new Student(
                request.FirstName,
                request.LastName,
                request.Address,
                request.Email,
                request.Program
            );
            
            await Repo.Save(newStudent);

            return newStudent;
        }
    }
}