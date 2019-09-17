using FluentNHibernate.Mapping;
using OlaDatabase.Entities;

namespace OlaDatabase.Mappings
{
    class PersonMapping : ClassMap<PersonEntity>
    {
        PersonMapping()
        {
            Table("persons");

            Id(x => x.PersonId, "personId").GeneratedBy.Increment();

            Map(x => x.FamilyName).Column("familyName").Not.Nullable();
            Map(x => x.FirstName).Column("firstName").Not.Nullable();
            Map(x => x.Sex, "sex").Nullable();
            Map(x => x.DateOfBirth, "dateOfBirth").Nullable();
            Map(x => x.Ssn, "ssn").Nullable();
            Map(x => x.Competitor, "competitor").Not.Nullable();
            References(x => x.DefaultOrganisation).Column("defaultOrganisationId").NotFound.Ignore().Nullable(); //No Foreign Key exists!
            Map(x => x.PersonIdInClub, "personIdInClub").Nullable();
            Map(x => x.VIPType, "VIPType").Nullable(); // Foreign Key - viptypes - Persons_FK01
            Map(x => x.NationalityId, "nationalityId").Nullable(); // Foreign Key - countries - Persons_FK00
            Map(x => x.ModifyDate, "modifyDate").Nullable();
            References(x => x.ModifiedBy, "modifiedBy").ForeignKey("Persons_FK02").Nullable();
            Map(x => x.Dead, "dead").Default("0").Not.Nullable();

            HasMany(x => x.Entries)
                .KeyColumn("competitorId")
                .ForeignKeyConstraintName("Entries_FK03")
                .Inverse();
        }
    }
}
