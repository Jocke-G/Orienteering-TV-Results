namespace OlaDatabase.Entities
{
    public class PersonEntity
    {
        public virtual int PersonId { get; set; }
        public virtual string FamilyName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual OrganisationEnity Organisation { get; set; }
    }
}
