using System.Transactions;
using Azure.Messaging.EventGrid;
using MediatR;
using Newtonsoft.Json;

namespace  eventschool
{
    public class StudentRegisteredStoreHandler : INotificationHandler<StudentRegistered>

    {
        private readonly IEventStoreRepository<DomainEvent<Student>> _eventstoreRepo;
        private readonly IOutboxRepository _outboxRepo;

        public StudentRegisteredStoreHandler(
            IEventStoreRepository<DomainEvent<Student>> eventstoreRepo,
            IOutboxRepository outboxRepo)
        {
            this._eventstoreRepo = eventstoreRepo;
            this._outboxRepo = outboxRepo;
        }
        public async Task Handle(StudentRegistered notification, CancellationToken cancellationToken)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    await _eventstoreRepo.Append(notification);                    
                    var outboxNotification = new OutboxNotification();
                    outboxNotification.Data = JsonConvert.SerializeObject(notification);
                    await _outboxRepo.Insert(outboxNotification);
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