using Azure.Messaging.EventGrid;
using MediatR;

namespace  eventschool
{
    public class StudentRegisteredNotificationHandler : INotificationHandler<StudentRegisteredNotification>

    {
        private readonly IMediator _mediator;

        public StudentRegisteredNotificationHandler(IMediator  mediator )
        {
            this._mediator = mediator;
        }
        public async Task Handle(StudentRegisteredNotification notification, CancellationToken cancellationToken)
        {

             await _mediator.Send(new EnrolToClass() 
             {ProgramId = notification.Program, StudentId = notification.StudentId});
        }
    }

}