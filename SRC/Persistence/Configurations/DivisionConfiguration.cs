using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Domain.Entities
{
    public class DivisionConfiguration : IEntityTypeConfiguration<Division>
    {
        public void Configure(EntityTypeBuilder<Division> builder)
        {
            builder.Property(e => e.DivisionId).HasColumnName("Division_ID");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.ShortName)
                .IsRequired()
                .HasColumnName("Short_Name")
                .HasMaxLength(6);

        }
    }
}
