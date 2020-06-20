using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Domain.Entities
{
    public class TurnoverConfiguration : IEntityTypeConfiguration<Turnover>
    {
        public void Configure(EntityTypeBuilder<Turnover> builder)
        {
            builder.HasIndex(e => e.IncidentId)
                     .HasName("UQ_Turnover_Incident_ID")
                     .IsUnique();

            builder.Property(e => e.TurnoverId).HasColumnName("Turnover_ID");

            builder.Property(e => e.IncidentId).HasColumnName("Incident_ID");

            builder.Property(e => e.PlayerId).HasColumnName("Player_ID");

            builder.Property(e => e.TurnoverType).HasColumnName("Turnover_Type");

            builder.HasOne(d => d.Incident)
                .WithOne(p => p.Turnover)
                .HasForeignKey<Turnover>(d => d.IncidentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Turnover_Incident_ID_Incident_Incident_ID");

            builder.HasOne(d => d.Player)
                .WithMany(p => p.Turnovers)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Turnover_Player_ID_Player_Player_ID");
        }
    }
}