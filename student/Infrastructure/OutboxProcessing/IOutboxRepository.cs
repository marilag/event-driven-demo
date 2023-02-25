namespace eventschool
{
    public interface IOutboxRepository 
    {
        Task Insert(OutboxNotification eventData);
        Task<IEnumerable<OutboxNotification>> GetUnprocessed();
        Task Process(OutboxNotification eventData);
        Task EvictProcessed();     


    }
}