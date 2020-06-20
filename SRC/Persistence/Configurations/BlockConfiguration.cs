using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Domain.Entities
{
    public class BlockConfiguration : IEntityTypeConfiguration<Block>
    {
        public void Configure(EntityTypeBuilder<Block> builder)
        {
            builder.HasIndex(e => e.ShotId)
                    .HasName("UQ_Block_Shot_ID")
                    .IsUnique();

            builder.Property(e => e.BlockId).HasColumnName("Block_ID");

            builder.Property(e => e.PlayerId).HasColumnName("Player_ID");

            builder.Property(e => e.ShotId).HasColumnName("Shot_ID");

            builder.HasOne(d => d.Player)
                    .WithMany(p => p.Blocks)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Block_Player_ID_Player_Player_ID");

            builder.HasOne(d => d.Shot)
                    .WithOne(p => p.Block)
                    .HasForeignKey<Block>(d => d.ShotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Block_Shot_ID_Shot_Shot_ID");
           
        }
    }
}
