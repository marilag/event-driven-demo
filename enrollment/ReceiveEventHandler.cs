using Azure.Messaging.EventGrid;
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
             logger.LogInformation($"Handle receive event started {request.EventData}");

             var e = JsonConvert.DeserializeAnonymousType(request.EventData, new {subject = "", data = new object()});
             
             logger.LogInformation($"Retrieved event subject {e?.subject}");

             var i = GetNotificationInstance(e?.subject,e?.data.ToString());

             logger.LogInformation($"Deserialized Event Instance {i}");             

             var stream = storeRepo.GetStream();

             var newProcess = new EnrollmentProcessInstance()
             {
                InstanceId = Guid.NewGuid(),
                CurrentState = ProcessState.Started
             };

             (ProcessState newState, IEnumerable<INotification> nextEvents) changedResult = newProcess.ChangeState(i);

             await mediator.Send(new ProcessStateChanged(
                newProcess.InstanceId,
                changedResult.newState,
                ProcessState.Started,
                i,
                changedResult.nextEvents));
            
        }

        private INotification? GetNotificationInstance(string? subject, string eventData) =>
            (subject) switch 
            {
                nameof(StudentRegistered) => JsonConvert.DeserializeObject<StudentRegistered>(eventData),
                _ => throw new Exception()
            };
    }

     
}