using eventschool;
using MediatR;
using Quartz;

public class ProcessOutbox : Quartz.IJob
{
    private readonly IOutboxRepository outbox;
    private readonly IMediator _mediator;

    
    public ProcessOutbox(
        IOutboxRepository outbox,
        IMediator mediator)
    {
        this.outbox = outbox;
        this._mediator = mediator;
    }


    public async Task Execute(IJobExecutionContext context)
    {
        var notifcations = outbox.GetUnprocessed();
        
        foreach (var n in notifcations)
        {
            await _mediator.Publish(n);     
            outbox.Process(n);  
        }
              
        return;
    }

    
}     