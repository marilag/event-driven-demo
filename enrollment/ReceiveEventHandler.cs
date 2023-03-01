using MediatR;
using Newtonsoft.Json;

namespace eventschool.enrollment
{
    public class ReceiveEventHandler : AsyncRequestHandler<ReceiveEvent>
    {
        private readonly IEventStoreRepository<DomainEvent<EnrollmentProcessInstance>> storeRepo;
        private readonly IMediator mediator;
        private readonly ILogger<ReceiveEventHandler> logger;

        public ReceiveEventHandler(IEventStoreRepository<DomainEvent<EnrollmentProcessInstance>> _storeRepo,
        IMediator mediator, 
        ILogger<ReceiveEventHandler> logger)
        {
            storeRepo = _storeRepo;
            this.mediator = mediator;
            this.logger = logger;
        }

        protected override async Task Handle(ReceiveEvent request, CancellationToken cancellationToken)
        {
             logger.LogInformation($"Handle receive event started {request.ReceivedEvent}");

             var e = JsonConvert.DeserializeAnonymousType(request.ReceivedEvent, new {subject = ""});
             
             logger.LogInformation($"Retrieved event subject {e?.subject}");

             var i = GetNotificationInstance(e?.subject,request);

             logger.LogInformation($"Deserialized Event Instance {i}");             

             var stream = storeRepo.GetStream();

             var newProcess = new EnrollmentProcessInstance()
             {
                InstanceId = Guid.NewGuid(),
                CurrentState = ProcessState.Started
             };

             (ProcessState newState, IEnumerable<INotification> newRequests) changedResult = newProcess.ChangeState(i);

             await mediator.Send(new ProcessStateChanged(
                newProcess.InstanceId,
                changedResult.newState,
                ProcessState.Started,
                i,
                changedResult.newRequests));
            
        }

        private INotification? GetNotificationInstance(string? subject, ReceiveEvent ReceivedEvent) =>
            (subject) switch 
            {
                nameof(StudentRegistered) =>  JsonConvert.DeserializeObject<StudentRegistered>(ReceivedEvent.ReceivedEvent),
                _ => throw new Exception()
            };
    }

     
}