using MediatR;

namespace eventschool
{
    public class RegisterStudentHandler : RequestHandler<RegisterStudent>
    {
        private readonly IMediator _mediator;   
        public IStudentRepository Repo { get; }

        public RegisterStudentHandler(IStudentRepository repo, IMediator mediator)
        {
            Repo = repo;
            this._mediator = mediator;
        }


       
        protected async override void Handle(RegisterStudent request)
        {
            Student newStudent = new() 
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                Email = request.Email,
                Program = request.Program
            };
            

            await _mediator.Publish<StudentRegistered>(new StudentRegistered(){
                StudentId = newStudent.StudentId.ToString(), 
                Program = newStudent.Program,
                Data = newStudent});
                    }
    }
}