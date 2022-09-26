namespace eventschool
{
    public class EventGridOptions 
    {
        public const string EventGrid = "EventGrid";

        public string TopicEndpoint { get; set; } = string.Empty;
        public string EventGridKey { get; set; } = string.Empty;

    }
}