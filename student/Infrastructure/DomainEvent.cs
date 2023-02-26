namespace eventschool
{
    public class DomainEvent<T>
    {
        public string EventType { get; set; } = string.Empty;
        public string SchemaVersion { get; set; } = String.Empty;

        public DateTime EventOccured { get; set; } = DateTime.UtcNow;                                                                                                                                                                                                 
        public T Data { get; set; } 

    }
}