using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Domain.Entities
{
    public class SeasonConfiguration : IEntityTypeConfiguration<Season>
    {
        public void Configure(EntityTypeBuilder<Season> builder)
        {
            builder.Property(e => e.SeasonId).HasColumnName("Season_ID");

            builder.Property(e => e.EndDate)
                .HasColumnName("End_date")
                .HasColumnType("date");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.StartDate)
                .HasColumnName("Start_date")
                .HasColumnType("date");
        }
    }
}