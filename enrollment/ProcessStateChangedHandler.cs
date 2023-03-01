using System.Transactions;
using MediatR;
using Newtonsoft.Json;

namespace  eventschool.enrollment
{
    public class ProcessStateChangedHandler : AsyncRequestHandler<ProcessStateChanged>

    {
        private readonly IEventStoreRepository<DomainEvent<EnrollmentProcessInstance>> _eventstoreRepo;
        private readonly IOutboxRepository _outboxRepo;
        private readonly ILogger<ProcessStateChanged> logger;

        public ProcessStateChangedHandler(
            IEventStoreRepository<DomainEvent<EnrollmentProcessInstance>> eventstoreRepo,
            IOutboxRepository outboxRepo,
            ILogger<ProcessStateChanged> logger)
        {
            this._eventstoreRepo = eventstoreRepo;
            this._outboxRepo = outboxRepo;
            this.logger = logger;
        }
        protected override async Task Handle(ProcessStateChanged notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Processs state change handler started {notification}");
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _eventstoreRepo.Append(notification); 
                    

                    foreach (var n in notification.TriggerCommands)
                    {
                        logger.LogInformation($"Get next request {n}");

                        var outboxNotification = new OutboxNotification();    

                        outboxNotification.Data = JsonConvert.SerializeObject(n);

                        _outboxRepo.Insert(outboxNotification);
                        
                        logger.LogInformation($"Next request put to outbox {_outboxRepo.GetUnprocessed().FirstOrDefault()}");

                    }                    

                    scope.Complete();

                }                
            }
            catch (TransactionAbortedException tex)
            {
                throw tex;
            }
        }


    }

}