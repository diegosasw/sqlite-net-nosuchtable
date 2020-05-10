using System;

namespace SqliteNetNoSuchTable.Model
{
    public class SomethingHappenedOne
        : IDomainEvent
    {
        public Guid AggregateId { get; }
        public DateTime CreatedOn { get; }
        public Guid CreatedBy { get; }

        public SomethingHappenedOne(Guid aggregateId, DateTime createdOn, Guid createdBy)
        {
            AggregateId = aggregateId;
            CreatedOn = createdOn;
            CreatedBy = createdBy;
        }
    }
}
