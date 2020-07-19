using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Domain.Entities
{
    public class PlayerSeasonConfiguration : IEntityTypeConfiguration<PlayerSeason>
    {
        public void Configure(EntityTypeBuilder<PlayerSeason> builder)
        {
            builder.HasKey(e => e.PlayerSeasonId)
                    .HasName("PK_Player_Season_Player_Season_ID");

            builder.ToTable("Player_Season");

            builder.Property(e => e.PlayerSeasonId).HasColumnName("Player_Season");

            builder.Property(e => e.Ast).HasColumnName("AST");

            builder.Property(e => e.Blk).HasColumnName("BLK");

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

            builder.Property(e => e.JerseyNr)
                .HasColumnName("Jersey_Nr")
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();

            builder.HasOne(d => d.Team)
                .WithMany(p => p.Players)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Player_Season_Team_ID_Season_Team_Team_ID");

            builder.Property(e => e.OffFouls).HasColumnName("OFF_FOULS");

            builder.Property(e => e.Orb).HasColumnName("ORB");

            builder.Property(e => e.PlayerId).HasColumnName("Player_ID");

            builder.Property(e => e.Pts)
                .HasColumnName("PTS")
                .HasComputedColumnSql("(((2)*[FG2M]+(3)*[FG3M])+[FTM])");

            builder.Property(e => e.SeasonDivisionId).HasColumnName("Season_Division_ID");

            builder.Property(e => e.Stl).HasColumnName("STL");

            builder.Property(e => e.TeamId).HasColumnName("Team_ID");

            builder.Property(e => e.Tov).HasColumnName("TOV");

            builder.Property(e => e.Trb)
                .HasColumnName("TRB")
                .HasComputedColumnSql("([ORB]+[DRB])");

            builder.HasOne(d => d.Player)
                .WithMany(p => p.PlayerSeasons)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Player_Season_Player_ID_Player_ID");

            builder.HasOne(d => d.SeasonDivision)
                .WithMany(p => p.PlayerSeasons)
                .HasForeignKey(d => d.SeasonDivisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Player_Season_Season_Division_ID_Season_Division_ID");
        }
    }
}