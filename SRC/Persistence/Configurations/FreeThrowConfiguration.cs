﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Domain.Entities
{
    public class FreeThrowConfiguration : IEntityTypeConfiguration<FreeThrow>
    {
        public void Configure(EntityTypeBuilder<FreeThrow> builder)
        {
            builder.ToTable("Free_Throw");

            builder.Property(e => e.FreeThrowId).HasColumnName("Free_Throw_ID");

            builder.Property(e => e.FoulId).HasColumnName("Foul_ID");

            builder.Property(e => e.PlayerShooterId).HasColumnName("Player_Shooter_ID");

            builder.HasOne(d => d.Foul)
                .WithMany(p => p.FreeThrows)
                .HasForeignKey(d => d.FoulId)
                .OnDelete(DeleteBehavior.Cascade) // cascade ??
                .HasConstraintName("FK_Free_Throw_Foul_ID_Foul_Foul_ID");

            builder.HasOne(d => d.PlayerShooter)
                .WithMany(p => p.FreeThrows)
                .HasForeignKey(d => d.PlayerShooterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Free_Throw_Player_Shooter_ID_Player_Player_ID");

        }
    }
}