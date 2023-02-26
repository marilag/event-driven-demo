using System.Collections;

namespace eventschool
{
    public class OutboxRepository : IEnumerable<OutboxNotification>, IOutboxRepository
    {
        public List<OutboxNotification> OutboxStore; 

        public OutboxRepository() => OutboxStore = new List<OutboxNotification>();

        public async Task<IEnumerable<OutboxNotification>> GetUnprocessed() => 
            OutboxStore.Where(o => o.IsProcessed == false);        

        public async Task Insert(OutboxNotification eventData) => OutboxStore.Add(eventData);      

        public async Task Process(OutboxNotification eventData) => 
            OutboxStore.Find(f => f.Equals(eventData)).IsProcessed = true;
        public Task EvictProcessed() =>
            throw new NotImplementedException();        

        public async Task<IEnumerable<OutboxNotification>> Get() =>  OutboxStore;
    
        public  IEnumerator<OutboxNotification> GetEnumerator() => OutboxStore.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>  OutboxStore.GetEnumerator();
    }
}