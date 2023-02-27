namespace eventschool
{
    public class DomainEvent<T>
    {
        public string EventType { get; init; } = default!;
        public  string SchemaVersion { get; set; } = default!;

        public DateTime EventOccured { get; set; } = DateTime.UtcNow;                                                                                                                                                                                                 
        public T Data { get; set; } 

    }
}