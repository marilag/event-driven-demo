namespace eventschool
{
    public class ServiceBusOptions{
        public const string ServiceBus = "ServiceBus";

        public string TopicPath { get; set; } = string.Empty;
        public string SubscriptionName { get; set; } = string.Empty;

        public string DeadLetterSubscriptionName { get; set; } = string.Empty;

    }
}