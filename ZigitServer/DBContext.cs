using ZigitApi.Entities;
using Microsoft.EntityFrameworkCore;
using ZigitApi.Utils;

//create DBCONTEXT TO CONNECT TO MYSQL SERVER

namespace ZigitApi
{
    public partial class DBContext : DbContext
    {

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Project> Projects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=admin;password=GLKatom113k;database=zigit");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //add configuration for creating new Models/Entities records in the database 
            modelBuilder.ApplyConfiguration(new UserCreateConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectCreateConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
