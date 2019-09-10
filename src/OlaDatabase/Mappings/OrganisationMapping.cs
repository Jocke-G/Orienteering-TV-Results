using FluentNHibernate.Mapping;
using OlaDatabase.Entities;

namespace OlaDatabase.Mappings
{
    class OrganisationMapping : ClassMap<OrganisationEnity>
    {
        public OrganisationMapping()
        {
            Table("organisations");

            Id(x => x.OrganisationId, "organisationId").GeneratedBy.Increment();

            Map(x => x.Name, "name").Not.Nullable();
            Map(x => x.ShortName, "shortName").Nullable();
        }
    }
}
