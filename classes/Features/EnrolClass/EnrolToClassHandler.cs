using MediatR;

namespace eventschool
{
    public class EnrolToClassHandler : IRequestHandler<EnrolToClass>
    {
        private readonly IClassesRepository classrepo;
        private readonly IMediator _mediator;

        public EnrolToClassHandler(IClassesRepository classrepo, IMediator mediator)
        {
            this.classrepo = classrepo;
            _mediator = mediator;
        }
        public async Task<Unit> Handle(EnrolToClass request, CancellationToken token)
        {
            var _classes = await classrepo.Get() ??
                throw new Exception("Class not found");

        foreach (var c in _classes.ToList())
        {
            c.AddStudent(request.StudentId);

            await classrepo.Save(c);

            await _mediator.Publish<ClassEnroled>(new ClassEnroled() {ClassCode = c.ClassCode, StudentId = request.StudentId});    

        }
        return new Unit();

        }
        
    }
}