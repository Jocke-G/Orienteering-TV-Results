using System;

namespace OlaDatabase.Entities
{
    public class EntryEntity
    {
        public virtual int EntryId { get; set; }
        public virtual int EventId { get; set; }
        public virtual string ExternalId { get; set; }
        public virtual int EntryOrganisationId { get; set; }
        public virtual string AllocationControl { get; set; }
        public virtual int AllocationEntryId { get; set; }
        public virtual int AllocationOrganisationId { get; set; }
        public virtual int AllocationPersonId { get; set; }
        public virtual int SeedingGroup { get; set; }
        public virtual PersonEntity Person { get; set; }
        public virtual int SportIdentCCardNumber { get; set; }
        public virtual int EmitCCardNumber { get; set; }
        public virtual int AcceptedEventClassId { get; set; }
        public virtual string TeamName { get; set; }
        public virtual string TeamLeader { get; set; }
        public virtual string BibNumber { get; set; }
        public virtual string Type { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime ModifyDate { get; set; }
        public virtual bool NoTotalResult { get; set; }
        public virtual int ModifiedBy { get; set; }
        public virtual string ResultTag { get; set; }
    }
}
