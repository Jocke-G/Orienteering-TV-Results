using System;
using System.Collections.Generic;

namespace OlaDatabase.Entities
{
    public class OrganisationEnity
    {
        public virtual int OrganisationId { get; set; }
        public virtual string Name { get; set; }
        public virtual string ShortName { get; set; }
        public virtual string Account { get; set; }
        public virtual int OrganisationTypeId { get; set; }
        public virtual int SuperOrganisationId { get; set; }
        public virtual int CountryId { get; set; }
        public virtual int OrganisationStatusId { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime MemberToDate { get; set; }
        public virtual string MediaName { get; set; }
        public virtual string ModifyDate { get; set; }
        public virtual PersonEntity ModifiedBy { get; set; }
        public virtual IEnumerable<EntryEntity> Entries { get; set; }

        public OrganisationEnity()
        {
            Entries = new List<EntryEntity>();
        }

        public OrganisationEnity(string name, int organisationTypeId, int countryId)
            :this()
        {
            Name = name;
            OrganisationTypeId = organisationTypeId;
            CountryId = countryId;
        }
    }
}
