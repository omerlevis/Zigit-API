
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZigitApi.Entities;


// Configuration for User creation

namespace ZigitApi.Utils
{
    public class UserCreateConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(45);

            builder.Property(e => e.Password)
                .IsRequired();

            builder.Property(e => e.Name)
                .IsRequired();

            builder.Property(e => e.Team)
                .IsRequired();

            builder.Property(e => e.joined)
                .IsRequired();

            builder.Property(e => e.Avatar)
                .IsRequired();
        }
    }
}