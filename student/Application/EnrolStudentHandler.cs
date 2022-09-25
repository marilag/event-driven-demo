using MediatR;

namespace student
{
    public class EnrolStudentHandler : IRequestHandler<EnrolStudent,Student>
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