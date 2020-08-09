using BasketballLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Persistence.Configurations
{
    public class SubstitutionConfiguration : IEntityTypeConfiguration<Substitution>
    {
        public void Configure(EntityTypeBuilder<Substitution> builder)
        {
            builder.HasIndex(e => e.IncidentId)
                    .HasName("UQ_Substitution_Incident_ID")
                    .IsUnique();

            builder.Property(e => e.Id).HasColumnName("Substitution_ID");

            builder.Property(e => e.IncidentId).HasColumnName("Incident_ID");

            builder.Property(e => e.PlayerInId).HasColumnName("Player_IN_ID");

            builder.Property(e => e.PlayerOutId).HasColumnName("Player_OUT_ID");

            builder.HasOne(d => d.Incident)
                .WithOne(p => p.Substitution)
                .HasForeignKey<Substitution>(d => d.IncidentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Substitution_Incident_ID_Incident_Incident_ID");

            builder.HasOne(d => d.PlayerIn)
                .WithMany(p => p.SubstitutionIn)
                .HasForeignKey(d => d.PlayerInId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Substitution_Player_IN_ID_Player_Player_ID");

            builder.HasOne(d => d.PlayerOut)
                .WithMany(p => p.SubstitutionOut)
                .HasForeignKey(d => d.PlayerOutId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Substitution_Player_OUT_ID_Player_Player_ID");
        }
    }
}