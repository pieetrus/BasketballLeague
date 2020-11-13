using BasketballLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Persistence.Configurations
{
    public class ReboundConfiguration : IEntityTypeConfiguration<Rebound>
    {
        public void Configure(EntityTypeBuilder<Rebound> builder)
        {
            builder
             .Property(e => e.ReboundType)
             .HasConversion<int>();

            builder.Property(e => e.Id).HasColumnName("Rebound_ID");

            builder.Property(e => e.PlayerId).HasColumnName("Player_ID");

            builder.Property(e => e.ReboundType).HasColumnName("Rebound_Type");

            builder.Property(e => e.TeamId).HasColumnName("Team_ID");


            builder.HasOne(d => d.Player)
                .WithMany(p => p.Rebounds)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK_Rebound_Player_ID_Player_Player_ID");

            builder.HasOne(d => d.Team)
                .WithMany(p => p.Rebounds)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK_Rebound_Team_ID_Team_Team_ID");

            builder.HasOne(d => d.FreeThrows)
                .WithOne(p => p.Rebound)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey<Rebound>(d => d.FreeThrowId);


            builder.HasOne(d => d.Shot)
                .WithOne(p => p.Rebound)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey<Rebound>(d => d.ShotId);
        }
    }
}