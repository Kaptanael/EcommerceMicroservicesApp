using Authentication.Domain.Common;
using Membership.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Infrastructure.Configurations
{
    internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable(nameof(RefreshToken));

            builder.HasKey(refreshToken => refreshToken.Token);

            builder.Property(refreshToken => refreshToken.Token).ValueGeneratedOnAdd();

            builder.Property(refreshToken => refreshToken.JwtId).IsRequired().HasMaxLength(1024);

            builder.Property(refreshToken => refreshToken.CreatedDate).IsRequired();

            builder.Property(refreshToken => refreshToken.ExpiryDate).IsRequired();

            builder.Property(refreshToken => refreshToken.Used).IsRequired();

            builder.Property(refreshToken => refreshToken.Invalidated).IsRequired();

            builder.Property(refreshToken => refreshToken.UserId).IsRequired();            
        }
    }
}
