using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Domain.Entities
{
    public class ReboundConfiguration : IEntityTypeConfiguration<Rebound>
    {
        public void Configure(EntityTypeBuilder<Rebound> builder)
        {
            builder
             .Property(e => e.ReboundType)
             .HasConversion<int>();

            builder.HasIndex(e => e.IncidentId)
                     .HasName("UQ_Rebound_Incident_ID")
                     .IsUnique();

            builder.Property(e => e.ReboundId).HasColumnName("Rebound_ID");

            builder.Property(e => e.IncidentId).HasColumnName("Incident_ID");

            builder.Property(e => e.PlayerId).HasColumnName("Player_ID");

            builder.Property(e => e.ReboundType).HasColumnName("Rebound_Type");

            builder.Property(e => e.TeamId).HasColumnName("Team_ID");

            builder.HasOne(d => d.Incident)
                .WithOne(p => p.Rebound)
                .HasForeignKey<Rebound>(d => d.IncidentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Rebound_Incident_ID_Incident_Incident_ID");

            builder.HasOne(d => d.Player)
                .WithMany(p => p.Rebounds)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK_Rebound_Player_ID_Player_Player_ID");

            builder.HasOne(d => d.Team)
                .WithMany(p => p.Rebounds)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK_Rebound_Team_ID_Team_Team_ID");
        }
    }
}