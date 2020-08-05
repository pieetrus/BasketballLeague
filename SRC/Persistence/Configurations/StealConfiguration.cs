using BasketballLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Persistence.Configurations
{
    public class StealConfiguration : IEntityTypeConfiguration<Steal>
    {
        public void Configure(EntityTypeBuilder<Steal> builder)
        {
            builder.HasIndex(e => e.TurnoverId)
                    .HasName("UQ_Steal_Turnover_ID")
                    .IsUnique();

            builder.Property(e => e.StealId).HasColumnName("Steal_ID");

            builder.Property(e => e.PlayerId).HasColumnName("Player_ID");

            builder.Property(e => e.TurnoverId).HasColumnName("Turnover_ID");

            builder.HasOne(d => d.Player)
                .WithMany(p => p.Steals)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Steal_Player_ID_Player_Player_ID");

            builder.HasOne(d => d.Turnover)
                .WithOne(p => p.Steal)
                .HasForeignKey<Steal>(d => d.TurnoverId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Steal_Turnover_ID_Turnover_Turnover_ID");
        }
    }
}