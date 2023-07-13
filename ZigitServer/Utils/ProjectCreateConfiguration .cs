
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZigitApi.Entities;

// // Configuration for Project creation


namespace ZigitApi.Utils
{
    public class ProjectCreateConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("projects");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .IsRequired();

            builder.Property(e => e.Score)
                .IsRequired();

            builder.Property(e => e.DurationInDays)
                .IsRequired();

            builder.Property(e => e.BugsCount)
                .IsRequired();

            builder.Property(e => e.MadeDeadline)
                .IsRequired();

            builder.Property(e => e.userId)
                .IsRequired(); 
        }
    }
}