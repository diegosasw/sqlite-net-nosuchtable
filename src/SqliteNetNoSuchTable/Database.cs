using SQLite;

namespace SqliteNetNoSuchTable
{
    public class Database
    {
        public SQLiteAsyncConnection Connection { get; }
        private const SQLiteOpenFlags Flags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache |
            SQLiteOpenFlags.ProtectionNone;
        public Database(string databasePath)
        {
            Connection = new SQLiteAsyncConnection(databasePath, Flags);
        }
    }
}
