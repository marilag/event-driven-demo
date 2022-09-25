using MediatR;

namespace eventschool
{
    public class EnrolToClassHandler : IRequestHandler<EnrolToClass,Class>
    {
        private readonly IClassesRepository classrepo;

        public EnrolToClassHandler(IClassesRepository classrepo)
        {
            this.classrepo = classrepo;
        }
        public async Task<Class> Handle(EnrolToClass request, CancellationToken token)
        {
            var _class = await classrepo.Get(request.ClassId) ??
                throw new Exception("Class not found");

            _class.AddStudent(request.StudentId);

            await classrepo.Save(_class);

            return _class;

        }
        
    }
}