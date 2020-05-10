using System;

namespace SqliteNetNoSuchTable.Model
{
    public class SomethingHappenedTwo
        : IDomainEvent
    {
        public Guid AggregateId { get; }
        public DateTime CreatedOn { get; }
        public bool IsSample { get; }

        public SomethingHappenedTwo(Guid aggregateId, DateTime createdOn, bool isSample)
        {
            AggregateId = aggregateId;
            CreatedOn = createdOn;
            IsSample = isSample;
        }
    }
}
