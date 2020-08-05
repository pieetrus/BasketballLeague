using BasketballLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Persistence.Configurations
{
    public class JumpBallConfiguration : IEntityTypeConfiguration<JumpBall>
    {
        public void Configure(EntityTypeBuilder<JumpBall> builder)
        {
            builder
              .Property(e => e.JumpBallType)
              .HasConversion<int>();

            builder.ToTable("Jump_Ball");

            builder.Property(e => e.JumpBallId).HasColumnName("Jump_Ball_ID");

            builder.Property(e => e.IncidentId).HasColumnName("Incident_ID");

            builder.Property(e => e.JumpBallType).HasColumnName("Jump_Ball_Type");

            builder.HasOne(d => d.Incident)
                .WithOne(p => p.JumpBall)
                .HasForeignKey<JumpBall>(d => d.IncidentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Jump_Ball_Incident_ID_Incident_Incident_ID");

        }
    }
}