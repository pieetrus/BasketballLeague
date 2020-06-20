using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Domain.Entities
{
    public class ShotConfiguration : IEntityTypeConfiguration<Shot>
    {
        public void Configure(EntityTypeBuilder<Shot> builder)
        {
            builder.HasIndex(e => e.IncidentId)
                   .HasName("UQ_Shot_Incident_ID")
                   .IsUnique();

            builder.Property(e => e.ShotId).HasColumnName("Shot_ID");

            builder.Property(e => e.IncidentId).HasColumnName("Incident_ID");

            builder.Property(e => e.IsAccurate).HasColumnName("Is_Accurate");

            builder.Property(e => e.IsFastAttack).HasColumnName("Is_Fast_Attack");

            builder.Property(e => e.PlayerId).HasColumnName("Player_ID");

            builder.Property(e => e.ShotType).HasColumnName("Shot_Type");

            builder.HasOne(d => d.Incident)
                .WithOne(p => p.Shot)
                .HasForeignKey<Shot>(d => d.IncidentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shot_Incident_ID_Incident_Incident_ID");

            builder.HasOne(d => d.Player)
                .WithMany(p => p.Shots)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shot_Player_ID_Player_Player_ID");
        }
    }
}