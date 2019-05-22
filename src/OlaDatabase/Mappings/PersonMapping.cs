using FluentNHibernate.Mapping;
using OlaDatabase.Entities;

namespace OlaDatabase.Mappings
{
    class PersonMapping: ClassMap<PersonEntity>
    {
        public PersonMapping()
        {
            Table("persons");

            Id(x => x.PersonId, "personId").GeneratedBy.Increment();

            Map(x => x.FamilyName).Column("familyName").Not.Nullable();
            Map(x => x.FirstName).Column("firstName").Not.Nullable();
        }
    }
}
