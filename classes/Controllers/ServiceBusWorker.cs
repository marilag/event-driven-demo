using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;

namespace eventschool
{
    
    public class ServiceBusWorker : IHostedService, IDisposable
    {
        private readonly ServiceBusClient _sbClient;
        private readonly ServiceBusOptions _options;
        private readonly ILogger<ServiceBusWorker> _logger;

        private  ServiceBusProcessor _processor;

        public ServiceBusWorker(ServiceBusClient sbClient,
            IOptions<ServiceBusOptions> options,
            ILogger<ServiceBusWorker> logger)
        {
            this._sbClient = sbClient;
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
            _logger.LogInformation($"Received message");

            var myPayload = args.Message.Body.ToString();

             _logger.LogInformation($"Received message {myPayload}");

            await args.CompleteMessageAsync(args.Message).ConfigureAwait(false);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _processor.CloseAsync().ConfigureAwait(false);
        }
    }
}