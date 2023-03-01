using eventschool;
using MediatR;
using Quartz;

namespace eventschool.enrollment
{


public class ProcessOutbox : Quartz.IJob
{
    private readonly IOutboxRepository outbox;
    private readonly IMediator _mediator;
        private readonly ILogger<ProcessOutbox> _logger;

        public ProcessOutbox(
        IOutboxRepository outbox,
        IMediator mediator,
        ILogger<ProcessOutbox> logger)
        
    {
        this.outbox = outbox;
        this._mediator = mediator;
        this._logger = logger;
        }


    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogTrace("Processing outbox messages");
        var notifcations = outbox.GetUnprocessed();
        _logger.LogTrace($"{notifcations?.Count()} notification messages found");
        foreach (var n in notifcations)
        {
            await _mediator.Publish(n);     
            outbox.Process(n);  
        }
        _logger.LogTrace("Processing outbox notification completed");
              
        return;
    }
}
    
}     