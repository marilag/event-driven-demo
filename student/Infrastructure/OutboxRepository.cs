using System.Collections;

namespace eventschool
{
    public class OutboxRepository<T> : IEnumerable<T>, IOutboxRepository<T> 
    {
        public List<T> OutboxStore; 

        public OutboxRepository() => OutboxStore = new List<T>();

        public async Task<IEnumerable<T>> GetUnprocessed() => OutboxStore;
        

        public async Task Insert(T eventData) => throw new Exception();      

        public async Task Process(T eventData) => OutboxStore.Remove(eventData);
        public Task EvictProcessed() 
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> Get() =>  OutboxStore;
    
        public  IEnumerator<T> GetEnumerator() => OutboxStore.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>  OutboxStore.GetEnumerator();
    }
}