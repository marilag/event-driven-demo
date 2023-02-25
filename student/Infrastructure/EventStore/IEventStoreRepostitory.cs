namespace eventschool
{
    public interface IEventStoreRepository<T> 
    {        
        Task Append(T eventData);
        Task<IEnumerable<T>> Get();
    }
}