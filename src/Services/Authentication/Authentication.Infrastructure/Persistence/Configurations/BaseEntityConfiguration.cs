using Membership.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Infrastructure.Configurations
{
    internal class BaseEntityConfiguration <TEntity>: IEntityTypeConfiguration<TEntity> where TEntity : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(entityBase => entityBase.Id);

            builder.Property(entityBase => entityBase.Id).ValueGeneratedOnAdd();

            builder.Property(entityBase => entityBase.CreatedBy).IsRequired();

            builder.Property(entityBase => entityBase.CreatedDate).IsRequired();

            builder.Property(entityBase => entityBase.ModifiedBy).IsRequired(false);

            builder.Property(entityBase => entityBase.ModifiedDate).IsRequired(false);
        }
    }
}
