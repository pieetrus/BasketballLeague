using BasketballLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Persistence.Configurations
{
    public class AssistConfiguration : IEntityTypeConfiguration<Assist>
    {
        public void Configure(EntityTypeBuilder<Assist> builder)
        {

            builder.HasIndex(e => e.ShotId)
                    .HasName("UQ_Assist_Shot_ID")
                    .IsUnique();

            builder.Property(e => e.Id).HasColumnName("Assist_ID");

            builder.Property(e => e.FreeThrowId).HasColumnName("Free_Throw_ID");

            builder.Property(e => e.PlayerId).HasColumnName("Player_ID");

            builder.Property(e => e.ShotId).HasColumnName("Shot_ID");

            builder.HasOne(d => d.FreeThrows)
                    .WithOne(p => p.Assist)
                    .HasForeignKey<Assist>(d => d.FreeThrowId)
                    .HasConstraintName("FK_Assist_Free_Throw_ID_Free_Throw_Free_Throw_ID");

            builder.HasOne(d => d.Player)
                    .WithMany(p => p.Assists)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Assist_Player_ID_Player_Player_ID");

            builder.HasOne(d => d.Shot)
                    .WithOne(p => p.Assist)
                    .HasForeignKey<Assist>(d => d.ShotId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Assist_Shot_ID_Shot_Shot_ID");
        }
    }
}
