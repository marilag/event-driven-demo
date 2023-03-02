using MediatR;

namespace eventschool
{
    public class IncreaseClassSizeHandler : IRequestHandler<IncreaseClassSize>
    {
        private readonly IClassesRepository classrepo;
        private readonly IMediator _mediator;

        public IncreaseClassSizeHandler(IClassesRepository classrepo, IMediator mediator)
        {
            this.classrepo = classrepo;
            _mediator = mediator;
        }
        public async Task<Unit> Handle(IncreaseClassSize request, CancellationToken token)
        {
            var _classes = await classrepo.Get() ??
                throw new Exception("Class not found");

        foreach (var c in _classes.ToList())
        {
            c.MaxStudents = c.MaxStudents + 3;

            await classrepo.Save(c);

            

        }
        return new Unit();

        }
        
    }
}