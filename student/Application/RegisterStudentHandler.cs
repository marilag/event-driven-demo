using MediatR;

namespace eventschool
{
    public class RegisterStudentHandler : IRequestHandler<RegisterStudent,Student>
    {
        private readonly IMediator _mediator;   
        public IStudentRepository Repo { get; }

        public RegisterStudentHandler(IStudentRepository repo, IMediator mediator)
        {
            Repo = repo;
            this._mediator = mediator;
        }


        public async Task<Student> Handle(RegisterStudent request, CancellationToken cancellationToken)
        {            
            var newStudent = new Student(
                request.FirstName,
                request.LastName,
                request.Address,
                request.Email,
                request.Program
            );

            await Repo.Save(newStudent);

            await _mediator.Publish<StudentRegistered>(new StudentRegistered(){
                StudentId = newStudent.StudentId.ToString(), 
                Program = newStudent.Program});
            
            return newStudent;
        }
    }
}