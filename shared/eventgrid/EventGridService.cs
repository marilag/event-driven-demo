using Azure;
using Azure.Messaging.EventGrid;
using Microsoft.Extensions.Options;

namespace eventschool.shared
{
    public interface IEventGridService
    {
        Task Publish(IList<EventGridEvent> events);
    }
    public class EventGridService : IEventGridService
    {
        private readonly EventGridPublisherClient _client;
        private readonly EventGridOptions _options;

        public EventGridService(IOptions<EventGridOptions> options)
        {
            
            _options = options.Value;
            _client = new EventGridPublisherClient(
            new Uri(_options.TopicEndpoint),
            new AzureKeyCredential(_options.EventGridKey));
        }
        public async Task Publish(IList<EventGridEvent> events)
        {
            await _client.SendEventsAsync(events);
        }
    }
}