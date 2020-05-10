using System;

namespace SqliteNetNoSuchTable.Model
{
    public interface IDomainEvent
    {
        Guid AggregateId { get; }
        DateTime CreatedOn { get; }
    }
}
