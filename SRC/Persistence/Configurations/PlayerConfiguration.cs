﻿using BasketballLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Persistence.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder
             .Property(e => e.Position)
             .HasConversion<int>();

            builder.Property(e => e.Id).HasColumnName("Player_ID");

            builder.Property(e => e.Birthdate).HasColumnType("date");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.PhotoUrl)
                .HasColumnName("Photo_URL")
                .HasMaxLength(300)
                .IsUnicode(false);

            builder.Property(e => e.Surname)
                .IsRequired()
                .HasMaxLength(30);

        }
    }
}