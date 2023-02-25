using MediatR;

namespace eventschool
{
    public class OutboxNotification : INotification
    {
        public string Type { get; set; } = string.Empty;
        public string Data { get; set; } = string.Empty;

        public bool IsProcessed { get; set; } = false;
    }
}