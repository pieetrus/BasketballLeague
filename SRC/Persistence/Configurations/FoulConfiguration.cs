using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Domain.Entities
{
    public class FoulConfiguration : IEntityTypeConfiguration<Foul>
    {
        public void Configure(EntityTypeBuilder<Foul> builder)
        {

            builder
               .Property(e => e.FoulType)
               .HasConversion<int>();

            builder.HasIndex(e => e.IncidentId)
                     .HasName("UQ_Foul_Incident_ID")
                     .IsUnique();

            builder.Property(e => e.FoulId).HasColumnName("Foul_ID");

            builder.Property(e => e.CoachId).HasColumnName("Coach_ID");

            builder.Property(e => e.FoulType).HasColumnName("Foul_Type");

            builder.Property(e => e.IncidentId).HasColumnName("Incident_ID");

            builder.Property(e => e.TeamId).HasColumnName("Team_ID");

            builder.Property(e => e.PlayerWhoFouledId).HasColumnName("Player_Who_Fouled_ID");

            builder.Property(e => e.PlayerWhoWasFouledId).HasColumnName("Player_Who_Was_Fouled_ID");

            builder.HasOne(d => d.Coach)
                    .WithMany(p => p.Foul)
                    .HasForeignKey(d => d.CoachId)
                    .HasConstraintName("FK_Foul_Coach_ID_Coach_Coach_ID");

            builder.HasOne(d => d.Incident)
                .WithOne(p => p.Foul)
                .HasForeignKey<Foul>(d => d.IncidentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Foul_Incident_ID_Incident_Incident_ID");

            builder.HasOne(d => d.PlayerWhoFouled)
                .WithMany(p => p.Fouls)
                .HasForeignKey(d => d.PlayerWhoFouledId)
                .HasConstraintName("FK_Foul_Player_Who_Fouled_ID_Player_Player_ID");

            builder.HasOne(d => d.PlayerWhoWasFouled)
                .WithMany(p => p.FoulsOn)
                .HasForeignKey(d => d.PlayerWhoWasFouledId)
                .HasConstraintName("FK_Foul_Player_Who_Was_Fouled_ID_Player_Player_ID");

            builder.HasOne(d => d.Team)
                .WithMany(p => p.BenchFouls)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK_Foul_Team_ID_Team_Team_ID");

        }
    }
}
