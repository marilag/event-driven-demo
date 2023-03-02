using Azure.Messaging.EventGrid;
using MediatR;
using Newtonsoft.Json;

namespace  eventschool.enrollment
{
    public class OutboxNotificationHandler :  INotificationHandler<OutboxNotification> 

    {
        private readonly IEventGridService _eventGridService;

        public OutboxNotificationHandler(IEventGridService eventGridService)
        {
            _eventGridService = eventGridService;
        }      
       
        public async Task Handle(OutboxNotification notification, CancellationToken cancellationToken)
        {
          
            var eventData = JsonConvert.DeserializeObject<DomainEvent<Student>>(notification.Data);
            await _eventGridService.Publish(new List<EventGridEvent> {
                new EventGridEvent(eventData.EventType,eventData.EventType,"1.0",eventData)                
            });       
        }
    }

}