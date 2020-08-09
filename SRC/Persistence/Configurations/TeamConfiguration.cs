using BasketballLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Persistence.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasIndex(e => e.Name)
                    .HasName("UQ_Team_Name")
                    .IsUnique();

            builder.Property(e => e.Id).HasColumnName("Team_ID");

            builder.Property(e => e.LogoUrl)
                .HasColumnName("Logo_URL")
                .HasMaxLength(300)
                .IsUnicode(false);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.ShortName)
                .IsRequired()
                .HasColumnName("Short_Name")
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
        }
    }
}