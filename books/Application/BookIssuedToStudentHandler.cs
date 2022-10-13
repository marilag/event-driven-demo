using Azure.Messaging.EventGrid;
using MediatR;

namespace eventschool
{
    public class BookIssuedToStudentHandler : INotificationHandler<BookIssuedToStudent>
    {
        private readonly IEventGridService _eventGridService;

        public BookIssuedToStudentHandler(IEventGridService eventGridService)
        {
            _eventGridService = eventGridService;
        }
        public async Task Handle(BookIssuedToStudent notification, CancellationToken cancellationToken)
        {
              await _eventGridService.Publish(new List<EventGridEvent> {
                new EventGridEvent(nameof(BookIssuedToStudent),nameof(BookIssuedToStudent),"1.0",notification)                
            });   
        }
    }
}