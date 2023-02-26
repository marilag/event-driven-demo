using MediatR;

namespace eventschool
{
    public class GetResgistrationsHandler : IRequestHandler<GetResgistrations,List<Student>>
    {
        public IEventStoreRepository<DomainEvent<Student>> _eventStore { get; }

        public GetResgistrationsHandler(IEventStoreRepository<DomainEvent<Student>> eventStore)
        {
            _eventStore = eventStore;
        }


        public Task<List<Student>> Handle(GetResgistrations request, CancellationToken cancellationToken)
        {            
             return Task.FromResult(_eventStore.GetStream().Select(e =>(Student)e.Data).ToList());
            
        }

        
    }
}