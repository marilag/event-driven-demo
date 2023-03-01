using MediatR;

namespace eventschool.enrollment

{
    public enum ProcessState { Started, Waiting, Completed, Failed  }

    public record EnrollmentProcessInstance
    {

        public Guid InstanceId { get; init; } = Guid.NewGuid();

        public ProcessState CurrentState { get; init; } = ProcessState.Started;

      
        public  (ProcessState, IEnumerable<INotification>?) ChangeState( INotification newEvent) =>
            (this.CurrentState, newEvent) switch
            {
                (ProcessState.Started, StudentRegistered) => (ProcessState.Waiting, NextCommands(newEvent)),
                
                (ProcessState.Waiting, ClassEnroled) => (ProcessState.Completed, NextCommands(newEvent)),
                
                _ => throw new NotSupportedException()
            };

        private static IEnumerable<INotification>? NextCommands(INotification newEvent) =>
            (newEvent) switch 
            {
                (StudentRegistered) => new List<INotification>() { new StudentRegisteredCompleted() { StudentId =  ((StudentRegistered)newEvent).StudentId}},                
                (ClassEnroled) => null, 
                _ => throw new NotSupportedException()

            };
    }


}