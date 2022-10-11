using Azure.Messaging.EventGrid;
using MediatR;

namespace eventschool
{
    public class ClassEnroledNotificationHandler : INotificationHandler<ClassEnroledNotification>
    {

        public ClassEnroledNotificationHandler(IEventGridService eventGridService)
        {
            
        }
        public async Task Handle(ClassEnroledNotification notification, CancellationToken cancellationToken)
        {
               
        }
    }
}