namespace eventschool
{
    public interface IOutboxRepository 
    {
        Task InsertAsync(OutboxNotification eventData);
        Task<IEnumerable<OutboxNotification>> GetUnprocessedAsync();
        Task ProcessAsync(OutboxNotification eventData);
        Task EvictProcessedAsync();     

        void Insert(OutboxNotification eventData);
        IEnumerable<OutboxNotification> GetUnprocessed();
        void Process(OutboxNotification eventData);
        void EvictProcessed();     


    }
}