using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SqliteNetNoSuchTable.IntegrationTests.TestSupport;
using SqliteNetNoSuchTable.Model;
using Xunit;

namespace SqliteNetNoSuchTable.IntegrationTests.MyFunctionalityTests
{
    public static class WriteInDbTests
    {
        public class Given_An_In_Memory_Sqlite_Database_When_Writing_On_It
            : Given_WhenAsync_Then_Test
        {
            private MyFunctionality _sut;
            private Guid _aggregateId;
            private MyRepository _repository;
            private DateTime _createdOn;
            private Guid _createdBy;
            private SomethingHappenedOne _expectedEvent;

            protected override void Given()
            {
                _aggregateId = Guid.NewGuid();
                _createdOn = DateTime.UtcNow;
                _createdBy = Guid.NewGuid();

                var databasePath = ":memory:";

                var serviceProvider =
                    Startup.Init(databasePath);

                _sut = serviceProvider.GetService<MyFunctionality>();

                _repository = serviceProvider.GetService<MyRepository>();

                _expectedEvent = new SomethingHappenedOne(_aggregateId, _createdOn, _createdBy);
            }

            protected override async Task WhenAsync()
            {
                await _sut.WriteInDb(_aggregateId, _createdOn, _createdBy);
            }

            [Fact]
            public async Task Then_It_Should_Have_Saved_ShiftStarted_Event_On_Stream()
            {
                var events = await _repository.GetEvents(_aggregateId);
                events.Should().HaveCount(1);
            }

            [Fact]
            public async Task Then_It_Should_Have_Retrieved_The_Expected_Data()
            {
                var events = await _repository.GetEvents(_aggregateId);
                ((SomethingHappenedOne)events.First()).Should().BeEquivalentTo(_expectedEvent);
            }

            [Fact]
            public async Task Then_It_Should_Have_The_Same_Aggregate_Id()
            {
                var events = await _repository.GetEvents(_aggregateId);
                ((SomethingHappenedOne)events.First()).AggregateId.Should().Be(_aggregateId);
            }

            [Fact]
            public async Task Then_It_Should_Have_The_Same_CreatedOn()
            {
                var events = await _repository.GetEvents(_aggregateId);
                ((SomethingHappenedOne)events.First()).CreatedOn.Should().BeSameDateAs(_createdOn);
            }

            [Fact]
            public async Task Then_It_Should_Have_The_Same_CreatedBy()
            {
                var events = await _repository.GetEvents(_aggregateId);
                ((SomethingHappenedOne)events.First()).CreatedBy.Should().Be(_createdBy);
            }
        }

        public class Given_An_Inexisting_Physical_Sqlite_Database_When_Writing_On_It
            : Given_WhenAsync_Then_Test
        {
            private MyFunctionality _sut;
            private Guid _aggregateId;
            private MyRepository _repository;
            private DateTime _createdOn;
            private Guid _createdBy;
            private SomethingHappenedOne _expectedEvent;

            protected override void Given()
            {
                _aggregateId = Guid.NewGuid();
                _createdOn = DateTime.UtcNow;
                _createdBy = Guid.NewGuid();

                var databasePath = $"{Guid.NewGuid()}.db3";

                var serviceProvider =
                    Startup.Init(databasePath);

                _sut = serviceProvider.GetService<MyFunctionality>();

                _repository = serviceProvider.GetService<MyRepository>();

                _expectedEvent = new SomethingHappenedOne(_aggregateId, _createdOn, _createdBy);
            }

            protected override async Task WhenAsync()
            {
                await _sut.WriteInDb(_aggregateId, _createdOn, _createdBy);
            }

            [Fact]
            public async Task Then_It_Should_Have_Saved_ShiftStarted_Event_On_Stream()
            {
                var events = await _repository.GetEvents(_aggregateId);
                events.Should().HaveCount(1);
            }

            [Fact]
            public async Task Then_It_Should_Have_Retrieved_The_Expected_Data()
            {
                var events = await _repository.GetEvents(_aggregateId);
                ((SomethingHappenedOne)events.First()).Should().BeEquivalentTo(_expectedEvent);
            }

            [Fact]
            public async Task Then_It_Should_Have_The_Same_Aggregate_Id()
            {
                var events = await _repository.GetEvents(_aggregateId);
                ((SomethingHappenedOne)events.First()).AggregateId.Should().Be(_aggregateId);
            }

            [Fact]
            public async Task Then_It_Should_Have_The_Same_CreatedOn()
            {
                var events = await _repository.GetEvents(_aggregateId);
                ((SomethingHappenedOne)events.First()).CreatedOn.Should().BeSameDateAs(_createdOn);
            }

            [Fact]
            public async Task Then_It_Should_Have_The_Same_CreatedBy()
            {
                var events = await _repository.GetEvents(_aggregateId);
                ((SomethingHappenedOne)events.First()).CreatedBy.Should().Be(_createdBy);
            }
        }
    }
}