﻿using BasketballLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Persistence.Configurations
{
    public class RefereeConfiguration : IEntityTypeConfiguration<Referee>
    {
        public void Configure(EntityTypeBuilder<Referee> builder)
        {
            builder.Property(e => e.Id).HasColumnName("Referee_ID");

            builder.Property(e => e.Birthdate).HasColumnType("date");

            builder.Property(e => e.JerseyNr).HasColumnName("Jersey_Nr");

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

            builder.Property(e => e.JerseyNr)
                .HasColumnName("Jersey_Nr")
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
        }
    }
}