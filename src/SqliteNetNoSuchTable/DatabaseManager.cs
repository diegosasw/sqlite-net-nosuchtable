using System;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using SqliteNetNoSuchTable.DatabaseModel;

namespace SqliteNetNoSuchTable
{
    public class DatabaseManager
    {
        private readonly Database _database;

        public DatabaseManager(Database database)
        {
            _database = database;
        }

        public async Task TryCreate()
        {
            await TryCreateTable(typeof(StreamData));
            await TryCreateTable(typeof(EventData));
        }

        private async Task TryCreateTable(Type tableType)
        {
            if (_database.Connection.TableMappings.All(m => m.MappedType.Name != tableType.Name))
            {
                await _database.Connection.CreateTablesAsync(CreateFlags.None, tableType).ConfigureAwait(false);
            }
        }

        private async Task Close()
        {
            await _database.Connection.CloseAsync();
        }
    }
}
