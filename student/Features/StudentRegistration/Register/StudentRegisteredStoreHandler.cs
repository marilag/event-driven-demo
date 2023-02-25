using System.Transactions;
using Azure.Messaging.EventGrid;
using MediatR;

namespace  eventschool
{
    public class StudentRegisteredStoreHandler : INotificationHandler<StudentRegistered>

    {
        private readonly IEventStoreRepository<DomainEvent<Student>> _eventstoreRepo;
        private readonly IOutboxRepository<DomainEvent<Student>> _outboxRepo;

        public StudentRegisteredStoreHandler(
            IEventStoreRepository<DomainEvent<Student>> eventstoreRepo,
            IOutboxRepository<DomainEvent<Student>> outboxRepo)
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
                    await _outboxRepo.Insert(notification);
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