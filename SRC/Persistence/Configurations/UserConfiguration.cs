using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketballLeague.Domain.Entities
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(e => e.Email)
                   .HasName("UQ_User_Email")
                   .IsUnique();

            builder.HasIndex(e => e.UserName)
                .HasName("UQ_User_User_Name")
                .IsUnique();

            builder.Property(e => e.UserId).HasColumnName("User_ID");

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(320);

            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(e => e.UserName)
                .IsRequired()
                .HasColumnName("User_Name")
                .HasMaxLength(60)
                .IsUnicode(false);
        }
    }
}