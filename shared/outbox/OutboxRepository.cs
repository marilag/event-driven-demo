using System.Collections;

namespace eventschool
{
    public class OutboxRepository : IEnumerable<OutboxNotification>, IOutboxRepository
    {
        public List<OutboxNotification> OutboxStore; 

        public OutboxRepository() => OutboxStore = new List<OutboxNotification>();

        public IEnumerable<OutboxNotification> GetUnprocessed() => 
            OutboxStore.Where(o => o.IsProcessed == false);        

        public void Insert(OutboxNotification eventData) => OutboxStore.Add(eventData);      

        public void Process(OutboxNotification eventData) => 
            OutboxStore.Find(f => f.Equals(eventData)).IsProcessed = true;
        public Task EvictProcessed() =>
            throw new NotImplementedException();        

        public IEnumerable<OutboxNotification> Get() =>  OutboxStore;
    
        public  IEnumerator<OutboxNotification> GetEnumerator() => OutboxStore.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>  OutboxStore.GetEnumerator();

        public Task InsertAsync(OutboxNotification eventData)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OutboxNotification>> GetUnprocessedAsync()
        {
            throw new NotImplementedException();
        }

        public Task ProcessAsync(OutboxNotification eventData)
        {
            throw new NotImplementedException();
        }

        public Task EvictProcessedAsync()
        {
            throw new NotImplementedException();
        }

        void IOutboxRepository.EvictProcessed()
        {
            throw new NotImplementedException();
        }
    }
}