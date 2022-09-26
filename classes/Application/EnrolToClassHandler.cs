using MediatR;

namespace eventschool
{
    public class EnrolToClassHandler : IRequestHandler<EnrolToClass,Class>
    {
        private readonly IClassesRepository classrepo;
        private readonly IMediator _mediator;

        public EnrolToClassHandler(IClassesRepository classrepo, IMediator mediator)
        {
            this.classrepo = classrepo;
            _mediator = mediator;
        }
        public async Task<Class> Handle(EnrolToClass request, CancellationToken token)
        {
            var _class = await classrepo.Get(request.ClassId) ??
                throw new Exception("Class not found");

            _class.AddStudent(request.StudentId);

            await classrepo.Save(_class);

            await _mediator.Publish<ClassEnroled>(new ClassEnroled() {ClassCode = _class.ClassCode, StudentId = request.StudentId});
            return _class;

        }
        
    }
}