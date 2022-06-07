namespace Membership.Domain.Contracts
{
    
    public abstract class EntityBase
    {   
        public string Id { get; protected set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
