using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLite;
using SqliteNetNoSuchTable.DatabaseModel;
using SqliteNetNoSuchTable.Model;

namespace SqliteNetNoSuchTable
{
    public class MyRepository
    {
        private readonly SQLiteAsyncConnection _db;

        public MyRepository(Database database)
        {
            _db = database.Connection;
        }

        public async Task<IEnumerable<IDomainEvent>> GetEvents(Guid aggregateId)
        {
            var query =
                _db.Table<EventData>()
                    .Where(x => x.StreamId == aggregateId);

            var eventsData = await query.ToListAsync();
            var events = eventsData.Select(EventDataToEventMapper);
            return events;
        }

        public async Task SaveEvents(Guid aggregateId, params IDomainEvent[] events)
        {
            var eventsData = events.Select(EventToEventDataMapper).ToList();
            var someQueryHere = _db.Table<EventData>().Where(x => x.StreamId == aggregateId);
            await someQueryHere.CountAsync();
            await _db.InsertAllAsync(eventsData);
        }

        private IDomainEvent EventDataToEventMapper(EventData eventData)
        {
            var eventName = eventData.Name;
            var payloadBytes = eventData.Payload;
            var payloadJson = Encoding.UTF8.GetString(payloadBytes);
            var domainEventType = GetEventType(eventName);
            if (domainEventType is null)
            {
                throw new KeyNotFoundException($"Unrecognized event {eventName}");
            }

            var domainEvent = JsonConvert.DeserializeObject(payloadJson, domainEventType);
            return (IDomainEvent)domainEvent;
        }

        private Type GetEventType(string eventName)
        {
            var supportedEvents =
                new List<Type>
                {
                    typeof(SomethingHappenedOne),
                    typeof(SomethingHappenedTwo)
                };

            var eventType =
                supportedEvents
                    .FirstOrDefault(x => x.Name.Equals(eventName, StringComparison.InvariantCultureIgnoreCase));
            return eventType;
        }

        

        private EventData EventToEventDataMapper(IDomainEvent domainEvent)
        {
            var payloadJson = JsonConvert.SerializeObject(domainEvent);
            var payloadBytes = Encoding.UTF8.GetBytes(payloadJson);
            var payloadBytesLength = payloadBytes.Length;
            var metadataJson = JsonConvert.SerializeObject(new { });
            var metadataBytes = Encoding.UTF8.GetBytes(metadataJson);
            var metadataBytesLength = metadataBytes.Length;

            var eventData =
                new EventData
                {
                    StreamId = domainEvent.AggregateId,
                    Name = domainEvent.GetType().Name,
                    CreatedOn = domainEvent.CreatedOn,
                    Payload = payloadBytes,
                    PayloadSizeBytes = payloadBytesLength,
                    Metadata = metadataBytes,
                    MetadataSizeBytes = metadataBytesLength
                };

            return eventData;
        }
    }
}
