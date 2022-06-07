namespace Membership.Domain.Contracts
{
    public interface IEntityBase<TId> : IEntityBase
    {
        public TId Id { get; set; }
    }

    public interface IEntityBase
    {
    }
}
