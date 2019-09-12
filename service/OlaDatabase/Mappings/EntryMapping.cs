using FluentNHibernate.Mapping;
using OlaDatabase.Entities;

namespace OlaDatabase.Mappings
{
    class EntryMapping: ClassMap<EntryEntity>
    {
        public EntryMapping()
        {
            Table("entries");

            Id(x => x.EntryId, "entryId").GeneratedBy.Increment();

            Map(x => x.EventId).Column("eventId").Not.Nullable();
            References(x => x.Person).Column("competitorId").Nullable();
        }
    }
}
