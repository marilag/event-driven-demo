using MediatR;

namespace eventschool

{
    public enum ProcessState { Started, Waiting, Completed, Failed  }

    public class EnrollmentProcessInstance
    {

        public Guid InstanceId { get; init; } = Guid.NewGuid();

        public ProcessState CurrentState { get; set; }

        public EnrollmentProcessInstance()
        {
        }
        public (ProcessState, IEnumerable<IRequest>?) ChangeState( INotification newEvent) =>
            (this.CurrentState, newEvent) switch
            {
                (ProcessState.Started, StudentRegistered) => (ProcessState.Waiting, NextCommands(newEvent)),
                (ProcessState.Waiting, ClassEnroled) => (ProcessState.Completed, NextCommands(newEvent)),
                
                _ => throw new NotSupportedException()
            };

        private static IEnumerable<IRequest>? NextCommands(INotification newEvent) =>
            (newEvent) switch 
            {
                (StudentRegistered) => new List<IRequest>() { new EnrolToClass() { StudentId = ((StudentRegistered)newEvent).StudentId }}, 
                (ClassEnroled) => null, 
                _ => throw new NotSupportedException()

            };
    }


}