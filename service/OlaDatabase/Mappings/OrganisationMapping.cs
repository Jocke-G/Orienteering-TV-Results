using FluentNHibernate.Mapping;
using OlaDatabase.Entities;

namespace OlaDatabase.Mappings
{
    class OrganisationMapping : ClassMap<OrganisationEnity>
    {
        OrganisationMapping()
        {
            Table("organisations");

            Id(x => x.OrganisationId, "organisationId").GeneratedBy.Increment();

            Map(x => x.Name, "name").Not.Nullable();
            Map(x => x.ShortName, "shortName").Nullable();
            Map(x => x.Account, "account").Nullable();
            Map(x => x.OrganisationTypeId, "organisationTypeId").Not.Nullable(); // Foreign Key - organisationtypes - Organisations_FK01
            Map(x => x.SuperOrganisationId, "superOrganisationId").Nullable();
            Map(x => x.CountryId, "countryId").Not.Nullable(); // Foreign Key - countries - Organisations_FK00
            Map(x => x.OrganisationStatusId, "organisationStatusId").Nullable(); // Foreign Key - organisationstatuses - Organisations_FK03
            Map(x => x.CreateDate, "createDate").Nullable();
            Map(x => x.MemberToDate, "memberToDate").Nullable();
            Map(x => x.MediaName, "mediaName").Nullable();
            Map(x => x.ModifyDate, "modifyDate").Nullable();
            References(x => x.ModifiedBy, "modifiedBy").ForeignKey("Organisations_FK02").Nullable();

            HasMany(x => x.Entries)
                .KeyColumn("entryOrganisationId")
                .ForeignKeyConstraintName("Entries_FK00")
                .Inverse();
        }
    }
}
