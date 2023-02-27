

using MediatR;

namespace eventschool
{
    public class ReceiveEvent : IRequest
    {
        public string ReceivedEvent { get; init; } = default!;
    }
}