using BasketballLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Persistence.Configurations
{
    public class SeasonDivisionConfiguration : IEntityTypeConfiguration<SeasonDivision>
    {
        public void Configure(EntityTypeBuilder<SeasonDivision> builder)
        {
            builder.ToTable("Season_Division");

            builder.Property(e => e.SeasonDivisionId).HasColumnName("Season_Division_ID");

            builder.Property(e => e.DivisionId).HasColumnName("Division_ID");

            builder.Property(e => e.SeasonId).HasColumnName("Season_ID");

            builder.Property(e => e.WinnerSeasonDivisionTeamId).HasColumnName("Winner_Season_Division_Team_ID");

            builder.HasOne(d => d.Division)
                .WithMany(p => p.SeasonDivisions)
                .HasForeignKey(d => d.DivisionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Season_Division_Division_ID_Division_Division_ID");

            builder.HasOne(d => d.Season)
                .WithMany(p => p.SeasonDivisions)
                .HasForeignKey(d => d.SeasonId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Season_Division_Season_ID_Season_Season_ID");

            builder.HasOne(d => d.WinnerSeasonDivisionTeam)
                .WithMany(p => p.SeasonDivisions)
                .HasForeignKey(d => d.WinnerSeasonDivisionTeamId)
                .HasConstraintName("FK_Winner_Season_Division_Team_Division_ID_Division_Division_ID");
        }
    }
}