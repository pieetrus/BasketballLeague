using BasketballLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Persistence.Configurations
{
    public class CoachConfiguration : IEntityTypeConfiguration<Coach>
    {
        public void Configure(EntityTypeBuilder<Coach> builder)
        {
            builder.Property(e => e.CoachId).HasColumnName("Coach_ID");

            builder.Property(e => e.Birthdate).HasColumnType("date");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.PhotoUrl)
                .HasColumnName("Photo_URL")
                .HasMaxLength(300)
                .IsUnicode(false);

            builder.Property(e => e.Surname)
                .IsRequired()
                .HasMaxLength(30);

        }
    }
}
