using Membership.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Infrastructure.Configurations
{
    internal class UserConfiguration:BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.ToTable(nameof(User));            

            builder.Property(user => user.UserName).IsRequired().HasMaxLength(64);

            builder.HasIndex(user => user.Email);

            builder.Property(user => user.Email).IsRequired().HasMaxLength(64);

            builder.Property(user => user.PasswordHash).IsRequired();

            builder.Property(user => user.PasswordSalt).IsRequired();

            //builder.HasOne(u => u.RefreshToken)
            //.WithOne(u => u.User)
            //.HasForeignKey<RefreshToken>(rt => rt.UserId);

        }        
    }
}
