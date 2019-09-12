namespace OlaDatabase.Entities
{
    public class EntryEntity
    {
        public virtual int EntryId { get; set; }
        public virtual int EventId { get; set; }
        public virtual PersonEntity Person { get; set; }
    }
}
