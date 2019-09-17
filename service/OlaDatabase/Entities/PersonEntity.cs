using System;
using System.Collections.Generic;

namespace OlaDatabase.Entities
{
    public class PersonEntity
    {
        public virtual int PersonId { get; set; }
        public virtual string FamilyName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string Sex { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual string Ssn { get; set; }
        public virtual bool Competitor { get; set; }
        public virtual OrganisationEnity DefaultOrganisation { get; set; }
        public virtual int PersonIdInClub { get; set; }
        public virtual int VIPType { get; set; }
        public virtual int NationalityId { get; set; }
        public virtual DateTime ModifyDate { get; set; }
        public virtual PersonEntity ModifiedBy { get; set; }
        public virtual bool Dead { get; set; }
        public virtual IEnumerable<EntryEntity> Entries { get; set; }

        public PersonEntity()
        {
            Entries = new List<EntryEntity>();
        }
    }
}
