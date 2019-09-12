using System;

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
        public virtual int ModifiedBy { get; set; }
    }
}