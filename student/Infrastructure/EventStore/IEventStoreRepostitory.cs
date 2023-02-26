namespace eventschool
{
    public interface IEventStoreRepository<T> 
    {   
        void Append(T eventData);
        IEnumerable<T> GetStream();     
        Task AppendAsync(T eventData);
        Task<IEnumerable<T>> GetStreamAsync();
    }
}