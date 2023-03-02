

using MediatR;

namespace eventschool
{
    public class ReceiveEvent : IRequest
    {
        public string EventData { get; init; } = default!;
    }
}