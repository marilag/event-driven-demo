using Azure.Messaging.EventGrid;
using MediatR;

namespace eventschool
{
    public class ClassEnroledHandler : INotificationHandler<ClassEnroled>
    {
        private readonly IEventGridService _eventGridService;

        public ClassEnroledHandler(IEventGridService eventGridService)
        {
            _eventGridService = eventGridService;
        }
        public async Task Handle(ClassEnroled notification, CancellationToken cancellationToken)
        {
            await _eventGridService.Publish(new List<EventGridEvent> {
                new EventGridEvent(nameof(ClassEnroled),nameof(ClassEnroled),"1.0",notification)                
            });      
        }
    }
}