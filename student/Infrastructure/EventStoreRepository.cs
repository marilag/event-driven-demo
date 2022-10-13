using System.Collections;

namespace eventschool
{
    public class EventStoreRepository<T> : IEnumerable<T>, IEventStoreRepository<T> 
    {
        public List<T> EventStore; 

        public EventStoreRepository()
        {
            EventStore = new List<T>();
        }

        public async Task Append(T eventData)
        {
            EventStore.Add(eventData);
        }

        public async Task<IEnumerable<T>> Get()
        {
            return EventStore;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return EventStore.GetEnumerator();
        }

    
        IEnumerator IEnumerable.GetEnumerator()
        {
            return EventStore.GetEnumerator();
        }
    }
}