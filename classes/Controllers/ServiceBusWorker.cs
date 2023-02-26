using Azure.Messaging.ServiceBus;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace eventschool
{
    
    public class ServiceBusWorker : IHostedService, IDisposable
    {
        private readonly ServiceBusClient _sbClient;
        private readonly IMediator _mediator;
        private readonly ServiceBusOptions _options;
        private readonly ILogger<ServiceBusWorker> _logger;

        private  ServiceBusProcessor _processor;
        private  ServiceBusProcessor _processorDeadLetter;

        public ServiceBusWorker(ServiceBusClient sbClient,
            IOptions<ServiceBusOptions> options,
            IMediator mediator,
            ILogger<ServiceBusWorker> logger)
        {
            this._sbClient = sbClient;
            this._mediator = mediator;
            this._options = options.Value;
            this._logger = logger;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);    
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Starting the service bus queue. Listening to topic {_options.TopicPath} through subscription {_options.SubscriptionName}");

            var processoptions = new ServiceBusProcessorOptions() 
            {
                
                AutoCompleteMessages = false,
                MaxConcurrentCalls =1
            };

            _processor = _sbClient.CreateProcessor(_options.TopicPath,_options.SubscriptionName,processoptions);

            _processor.ProcessMessageAsync +=  ProcessMessagesAsync;  

            _processor.ProcessErrorAsync +=  ProcessErrorMessagesAsync;     


            await _processor.StartProcessingAsync().ConfigureAwait(false);    
            
        }

       

        private async Task ProcessErrorMessagesAsync(ProcessErrorEventArgs args)
        {

            _logger.LogDebug($"Error message {args.Exception.Message}");

        }

        private async Task ProcessMessagesAsync(ProcessMessageEventArgs args)
        {
             _logger.LogInformation($"EVENT RECIEVED: {args.Message.Body.ToString()}");
        
            var notification = JsonConvert.DeserializeObject<EventGridSchema<StudentRegisteredNotification>>(args.Message.Body.ToString());
            
            try
            {
                await _mediator.Publish(
                new StudentRegisteredNotification {
                    Program = notification.Data.Program,
                    StudentId   = notification.Data.StudentId});
                
            }
            catch (System.Exception ex)
            {
                
                await args.DeadLetterMessageAsync(args.Message ,ex.Message);
            }

            
            await args.CompleteMessageAsync(args.Message).ConfigureAwait(false);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _processor.CloseAsync().ConfigureAwait(false);
        }
    }
}