using Azure.Messaging.EventGrid;
using MediatR;

namespace  eventschool
{
    public class StudentRegisteredStoreHandler : INotificationHandler<StudentRegistered>

    {
        private readonly IEventStoreRepository<DomainEvent<Student>> _eventstoreRepo;

        public StudentRegisteredStoreHandler(IEventStoreRepository<DomainEvent<Student>> eventstoreRepo)
        {
            this._eventstoreRepo = eventstoreRepo;
        }
        public async Task Handle(StudentRegistered notification, CancellationToken cancellationToken)
        {
            await _eventstoreRepo.Append(notification);
        }
    }

}