using BasketballLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Persistence.Configurations
{
    public class TimeoutConfiguration : IEntityTypeConfiguration<Timeout>
    {
        public void Configure(EntityTypeBuilder<Timeout> builder)
        {
            builder.HasIndex(e => e.IncidentId)
                    .HasName("UQ_Timeout_Incident_ID")
                    .IsUnique();

            builder.Property(e => e.TimeoutId).HasColumnName("Timeout_ID");

            builder.Property(e => e.IncidentId).HasColumnName("Incident_ID");

            builder.Property(e => e.TeamId).HasColumnName("Team_ID");

            builder.HasOne(d => d.Incident)
                .WithOne(p => p.Timeout)
                .HasForeignKey<Timeout>(d => d.IncidentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Timeout_Incident_ID_Incident_Incident_ID");

            builder.HasOne(d => d.Team)
                .WithMany(p => p.Timeouts)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Timeout_Team_ID_Team_Team_ID");
        }
    }
}