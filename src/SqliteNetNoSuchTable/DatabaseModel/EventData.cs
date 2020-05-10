namespace SqliteNetNoSuchTable.DatabaseModel
{
    public class EventData
        : SqliteTable
    {
        public string Name { get; set; }
        public byte[] Payload { get; set; }
        public int PayloadSizeBytes { get; set; }
        public byte[] Metadata { get; set; }
        public int MetadataSizeBytes { get; set; }
    }
}
