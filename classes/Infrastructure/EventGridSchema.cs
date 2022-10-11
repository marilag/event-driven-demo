namespace eventschool
{
    public class EventGridSchema<T>
    {
        public string Topic { get; set; } = String.Empty;
        public string Subject { get; set; } = String.Empty;
        public string EventType { get; set; } = String.Empty;
        public string EventTime { get; set; } = String.Empty;
        public T Data { get; set; } 
    }
}