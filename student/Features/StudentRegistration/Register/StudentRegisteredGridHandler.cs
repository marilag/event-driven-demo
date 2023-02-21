using Azure.Messaging.EventGrid;
using MediatR;

namespace  eventschool
{
    public class StudentRegisteredGridHandler : INotificationHandler<StudentRegistered>

    {
        private readonly IEventGridService _eventGridService;

        public StudentRegisteredGridHandler(IEventGridService eventGridService)
        {
            _eventGridService = eventGridService;
        }
        public async Task Handle(StudentRegistered notification, CancellationToken cancellationToken)
        {
            await _eventGridService.Publish(new List<EventGridEvent> {
                new EventGridEvent(nameof(StudentRegistered),nameof(StudentRegistered),"1.0",notification)                
            });
        }
    }

}