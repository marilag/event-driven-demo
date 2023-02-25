namespace eventschool
{
    public interface IOutboxRepository<T> 
    {
        Task Insert(T eventData);
        Task<IEnumerable<T>> GetUnprocessed();
        Task Process(T eventData);
        Task EvictProcessed();     


    }
}