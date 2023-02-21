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
            Student newStudent = new() 
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                Email = request.Email,
                Program = request.Program
            };
              
            await Repo.Save(newStudent);

            await _mediator.Publish<StudentRegistered>(new StudentRegistered(){
                StudentId = newStudent.StudentId.ToString(), 
                Program = newStudent.Program,
                Data = newStudent});
            
            return newStudent;
        }
    }
}