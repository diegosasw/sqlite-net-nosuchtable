using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SqliteNetNoSuchTable.Model;

namespace SqliteNetNoSuchTable
{
    public class MyFunctionality
    {
        private readonly MyRepository _myRepository;

        public MyFunctionality(MyRepository myRepository)
        {
            _myRepository = myRepository;
        }

        public async Task WriteInDb(Guid aggregateId, DateTime createdOn, Guid createdBy)
        {
            var sampleEvent = new SomethingHappenedOne(aggregateId, createdOn, createdBy);
            await _myRepository.SaveEvents(aggregateId, sampleEvent);
        }

        public Task<IEnumerable<IDomainEvent>> ReadFromDb(Guid aggregateId)
        {
            return _myRepository.GetEvents(aggregateId);
        }
    }
}
