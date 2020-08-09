using BasketballLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Persistence.Configurations
{
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.Property(e => e.Id).HasColumnName("Match_ID");

            builder.Property(e => e.SeasonDivisionId).HasColumnName("Season_Division_ID");

            builder.Property(e => e.StartDate)
                .HasColumnName("Start_Date")
                .HasColumnType("smalldatetime");

            builder.Property(e => e.TeamGuestId).HasColumnName("Team_Guest_ID");

            builder.Property(e => e.TeamHomeId).HasColumnName("Team_Home_ID");

            builder.HasOne(d => d.SeasonDivision)
                .WithMany(p => p.Matches)
                .HasForeignKey(d => d.SeasonDivisionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Match_Season_Division_ID_Season_Division_Season_Division_ID");

            builder.HasOne(d => d.TeamGuest)
                .WithMany(p => p.MatchesAway)
                .HasForeignKey(d => d.TeamGuestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Match_Team_Guest_ID_Team_Team_ID");

            builder.HasOne(d => d.TeamHome)
                .WithMany(p => p.MatchesHome)
                .HasForeignKey(d => d.TeamHomeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Match_Team_Home_ID_Team_Team_ID");

        }
    }
}