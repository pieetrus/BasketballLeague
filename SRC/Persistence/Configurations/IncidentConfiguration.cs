using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Domain.Entities
{
    public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
    {
        public void Configure(EntityTypeBuilder<Incident> builder)
        {
            builder
                .Property(e => e.IncidentType)
                .HasConversion<int>();

            builder.Property(e => e.IncidentId).HasColumnName("Incident_ID");

            builder.Property(e => e.IncidentType).HasColumnName("Incident_type");

            builder.Property(e => e.MatchId).HasColumnName("Match_ID");

            builder.Property(e => e.Minutes)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();

            builder.Property(e => e.Seconds)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();

            builder.HasOne(d => d.Match)
                .WithMany(p => p.Incidents)
                .HasForeignKey(d => d.MatchId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Incident_Match_ID_Match_Match_ID");

        }
    }
}