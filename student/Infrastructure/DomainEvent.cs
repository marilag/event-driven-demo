namespace eventschool
{
    public class DomainEvent<T>
    {
        public string EventType { get; set; } = string.Empty;
        public string SchemaVersion { get; set; } = String.Empty;
        public T Data { get; set; } 

    }
}