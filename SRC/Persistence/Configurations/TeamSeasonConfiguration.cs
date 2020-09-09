using BasketballLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Persistence.Configurations
{
    public class TeamSeasonConfiguration : IEntityTypeConfiguration<TeamSeason>
    {
        public void Configure(EntityTypeBuilder<TeamSeason> builder)
        {
            builder.ToTable("Team_Season");

            builder.HasIndex(e => new { e.TeamId, e.SeasonDivisionId })
                .HasName("UQ_Team_Season_Player_ID_Season_Division_ID")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnName("Team_Season_ID");

            builder.Property(e => e.Ast).HasColumnName("AST");

            builder.Property(e => e.Blk).HasColumnName("BLK");

            builder.Property(e => e.CapitainId).HasColumnName("Capitain_ID");

            builder.Property(e => e.CoachId).HasColumnName("Coach_ID");

            builder.Property(e => e.Drb).HasColumnName("DRB");

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

            builder.Property(e => e.OffFouls).HasColumnName("OFF_FOULS");

            builder.Property(e => e.Orb).HasColumnName("ORB");

            builder.Property(e => e.Pts)
                .HasColumnName("PTS")
                .HasComputedColumnSql("(((2)*[FG2M]+(3)*[FG3M])+[FTM])");

            builder.Property(e => e.RankingPoints).HasColumnName("Ranking_points");

            builder.Property(e => e.SeasonDivisionId).HasColumnName("Season_Division_ID");

            builder.Property(e => e.Stl).HasColumnName("STL");

            builder.Property(e => e.TeamId).HasColumnName("Team_ID");

            builder.Property(e => e.Tov).HasColumnName("TOV");

            builder.Property(e => e.Trb)
                .HasColumnName("TRB")
                .HasComputedColumnSql("([ORB]+[DRB])");

            builder.HasOne(d => d.Capitain)
                .WithMany(p => p.TeamSeasons)
                .HasForeignKey(d => d.CapitainId)
                .HasConstraintName("FK_Team_Season_Capitain_ID_Player_Player_ID");

            builder.HasOne(d => d.Coach)
                .WithMany(p => p.TeamSeasons)
                .HasForeignKey(d => d.CoachId)
                .HasConstraintName("FK_Team_Season_Coach_ID_Coach_Coach_ID");

            builder.HasOne(d => d.SeasonDivision)
                .WithMany(p => p.TeamSeasons)
                .HasForeignKey(d => d.SeasonDivisionId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Team_Season_Season_Division_ID_Season_Division_ID");

            builder.HasOne(d => d.Team)
                .WithMany(p => p.TeamSeasons)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Team_Season_Team_ID_Team_ID");

        }
    }
}