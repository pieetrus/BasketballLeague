using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Domain.Entities
{
    public class RefereeMatchesConfiguration : IEntityTypeConfiguration<RefereeMatches>
    {
        public void Configure(EntityTypeBuilder<RefereeMatches> builder)
        {
            builder.HasKey(e => new { e.RefereeId, e.MatchId })
                     .HasName("PK_Referee_Matches_Referee_ID_Match_ID");

            builder.ToTable("Referee_Matches");

            builder.Property(e => e.RefereeId)
                .HasColumnName("Referee_ID")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.MatchId).HasColumnName("Match_ID");

            builder.HasOne(d => d.Match)
                .WithMany(p => p.RefereeMatches)
                .HasForeignKey(d => d.MatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Referee_Matches_Match_ID_Match_Match_ID");

            builder.HasOne(d => d.Referee)
                .WithMany(p => p.RefereeMatches)
                .HasForeignKey(d => d.RefereeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Referee_Matches_Referee_ID_Referee_Referee_ID");
        }
    }
}