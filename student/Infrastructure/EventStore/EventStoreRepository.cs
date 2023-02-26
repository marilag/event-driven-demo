using System.Collections;

namespace eventschool
{
    public class EventStoreRepository<T> : IEnumerable<T>, IEventStoreRepository<T> 
    {
        public List<T> EventStore; 

        public EventStoreRepository() => EventStore = new List<T>();

        public void Append(T eventData) => EventStore.Add(eventData);

        public IEnumerable<T> GetStream() =>  EventStore;

        public Task AppendAsync(T eventData)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetStreamAsync()
        {
            throw new NotImplementedException();
        }

        public  IEnumerator<T> GetEnumerator() => EventStore.GetEnumerator();
    
        IEnumerator IEnumerable.GetEnumerator() =>  EventStore.GetEnumerator();
    }
}