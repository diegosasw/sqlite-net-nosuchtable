using System;
using SQLite;

namespace SqliteNetNoSuchTable.DatabaseModel
{
    public class SqliteTable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public Guid StreamId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
