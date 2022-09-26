using MediatR;

namespace eventschool
{
    public class EnrolStudentHandler : IRequestHandler<EnrolStudent,Student>
    {
        private readonly IMediator _mediator;

        public EnrolStudentHandler(IStudentRepository repo, IMediator mediator)
        {
            Repo = repo;
            this._mediator = mediator;
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

            await _mediator.Publish<StudentRegistered>(new StudentRegistered(){StudentId = newStudent.StudentId.ToString(), Program = newStudent.Program});
            
            return newStudent;
        }
    }
}