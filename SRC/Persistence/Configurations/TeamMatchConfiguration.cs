using BasketballLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Persistence.Configurations
{
    public class TeamMatchConfiguration : IEntityTypeConfiguration<TeamMatch>
    {
        public void Configure(EntityTypeBuilder<TeamMatch> builder)
        {
            builder.ToTable("Team_Match");

            //builder.HasIndex(e => new { e.TeamId, e.MatchId })
            //    .HasName("UQ_Team_Match_Team_ID_Match_ID")
            //    .IsUnique();

            builder.Property(e => e.Id).HasColumnName("Team_Match_ID");

            builder.Property(e => e.Ast).HasColumnName("AST");

            builder.Property(e => e.BenchPts).HasColumnName("BENCH_PTS");

            builder.Property(e => e.Blk).HasColumnName("BLK");

            builder.Property(e => e.Drb).HasColumnName("DRB");

            builder.Property(e => e.Fastbreakpoints).HasColumnName("FASTBREAKPOINTS");

            builder.Property(e => e.Fg2a).HasColumnName("FG2A");

            builder.Property(e => e.Fg2m).HasColumnName("FG2M");

            builder.Property(e => e.Fg3a).HasColumnName("FG3A");

            builder.Property(e => e.Fg3m).HasColumnName("FG3M");

            builder.Property(e => e.Fga)
                .HasColumnName("FGA")
                .HasComputedColumnSql("([FG3A]+[FG2A])");

            builder.Property(e => e.Fgm)
                .HasColumnName("FGM")
                .HasComputedColumnSql("([FG3M]+[FG2M])");

            builder.Property(e => e.Fouls).HasColumnName("FOULS");

            builder.Property(e => e.Fta).HasColumnName("FTA");

            builder.Property(e => e.Ftm).HasColumnName("FTM");

            //builder.Property(e => e.MatchId).HasColumnName("Match_ID");

            builder.Property(e => e.OffFouls).HasColumnName("OFF_FOULS");

            builder.Property(e => e.Orb).HasColumnName("ORB");

            builder.Property(e => e.PointsFromTurnovers).HasColumnName("POINTS_FROM_TURNOVERS");

            builder.Property(e => e.Pts)
                .HasColumnName("PTS")
                .HasComputedColumnSql("(((2)*[FG2M]+(3)*[FG3M])+[FTM])");

            builder.Property(e => e.SecondChancePoints).HasColumnName("SECOND_CHANCE_POINTS");

            builder.Property(e => e.Stl).HasColumnName("STL");

            builder.Property(e => e.TeamId).HasColumnName("Team_ID");

            builder.Property(e => e.Tov).HasColumnName("TOV");

            builder.Property(e => e.Trb)
                .HasColumnName("TRB")
                .HasComputedColumnSql("([ORB]+[DRB])");

            builder.HasMany(d => d.MatchesHome)
                .WithOne(p => p.TeamHome)
                .HasForeignKey(d => d.TeamHomeId)
                .OnDelete(DeleteBehavior.Cascade);
            //.HasConstraintName("FK_Team_Match_Match_ID_Match_ID");

            builder.HasMany(d => d.MatchesAway)
                .WithOne(p => p.TeamGuest)
                .HasForeignKey(d => d.TeamGuestId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Team)
                .WithMany(p => p.TeamMatches)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Team_Match_Team_ID_Team_ID");
        }
    }
}