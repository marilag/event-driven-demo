using MediatR;

namespace eventschool.enrollment
{
    public class ProcessStateChanged : DomainEvent<EnrollmentProcessInstance>, IRequest
    {

        private const string _schemaVersion = "1.0";
        public Guid InstanceId { get; init;}
        public ProcessState NewState { get; init;}
        public ProcessState OldState { get; init;}
        public INotification TriggeredByEvent { get; init; }
        public IEnumerable<INotification> TriggerNewEvents { get; init;}
        
        public ProcessStateChanged(Guid instanceId, ProcessState newState, ProcessState oldState, INotification triggeredByEvent, IEnumerable<INotification> triggerCommands)
        {   
            EventType = nameof(ProcessStateChanged);
            SchemaVersion = _schemaVersion;
            InstanceId = instanceId;
            NewState = newState;
            OldState = oldState;
            TriggeredByEvent = triggeredByEvent;
            TriggerNewEvents = triggerCommands;
        }

     
    }

}