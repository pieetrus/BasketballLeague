using BasketballLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Persistence.Configurations
{
    public class FreeThrowConfiguration : IEntityTypeConfiguration<FreeThrows>
    {
        public void Configure(EntityTypeBuilder<FreeThrows> builder)
        {
            builder.ToTable("Free_Throw");

            builder.Property(e => e.Id).HasColumnName("Free_Throw_ID");

            builder.Property(e => e.FoulId).HasColumnName("Foul_ID");

            builder.Property(e => e.PlayerShooterId).HasColumnName("Player_Shooter_ID");

            builder.HasOne(d => d.Foul)
                .WithOne(p => p.FreeThrows)
                .HasForeignKey<FreeThrows>(d => d.FoulId)
                .OnDelete(DeleteBehavior.Cascade) // cascade ??
                .HasConstraintName("FK_Free_Throw_Foul_ID_Foul_Foul_ID");

            builder.HasOne(d => d.PlayerShooter)
                .WithMany(p => p.FreeThrows)
                .HasForeignKey(d => d.PlayerShooterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Free_Throw_Player_Shooter_ID_Player_Player_ID");

        }
    }
}