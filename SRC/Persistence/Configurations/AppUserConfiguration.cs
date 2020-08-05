using BasketballLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Persistence.Configurations
{

    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(e => e.Bio)
                .HasMaxLength(300);

            builder.Property(e => e.DisplayName)
                .HasMaxLength(35);

            builder.HasOne(d => d.Player)
                .WithOne(p => p.AppUser)
                .HasForeignKey<AppUser>(d => d.PlayerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
