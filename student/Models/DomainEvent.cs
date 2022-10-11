namespace eventschool
{
    public class DomainEvent<T>
    {
        public string EventType { get; set; } = typeof(T).ToString();
        public string DataVersion { get; set; } = String.Empty;
        public T Data { get; set; } 

    }
}