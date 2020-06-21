using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Domain.Entities
{
    public class PlayerMatchConfiguration : IEntityTypeConfiguration<PlayerMatch>
    {
        public void Configure(EntityTypeBuilder<PlayerMatch> builder)
        {
            builder.ToTable("Player_Match");

            builder.HasIndex(e => new { e.PlayerId, e.MatchId })
                .HasName("UQ_Player_Match_Player_ID_Match_ID")
                .IsUnique();

            builder.Property(e => e.PlayerMatchId).HasColumnName("Player_Match_ID");

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

            builder.Property(e => e.MatchId).HasColumnName("Match_ID");

            builder.Property(e => e.OffFouls).HasColumnName("OFF_FOULS");

            builder.Property(e => e.Orb).HasColumnName("ORB");

            builder.Property(e => e.PlayerId).HasColumnName("Player_ID");

            builder.Property(e => e.Pts)
                .HasColumnName("PTS")
                .HasComputedColumnSql("(((2)*[FG2M]+(3)*[FG3M])+[FTM])");

            builder.Property(e => e.Stl).HasColumnName("STL");

            builder.Property(e => e.Tov).HasColumnName("TOV");

            builder.Property(e => e.Trb)
                .HasColumnName("TRB")
                .HasComputedColumnSql("([ORB]+[DRB])");

            builder.HasOne(d => d.Match)
                .WithMany(p => p.PlayerMatches)
                .HasForeignKey(d => d.MatchId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Player_Match_Match_ID_Match_ID");

            builder.HasOne(d => d.Player)
                .WithMany(p => p.PlayerMatches)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Player_Match_Player_ID_Player_ID");

        }
    }
}