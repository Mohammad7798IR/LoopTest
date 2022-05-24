using Microsoft.EntityFrameworkCore;
using Test.Host.Entities;


namespace TestProject.Host.Context
{
    public partial class TestDBContext : DbContext
    {
        public TestDBContext(DbContextOptions<TestDBContext> options) : base(options)
        {

        }
    }
    public partial class TestDBContext
    {
        public DbSet<ApplicationUser> Users { get; set; }
    }

    public partial class TestDBContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ApplicationUser>().ToTable("User");

            modelBuilder.Entity<ApplicationUser>().HasKey(pk=>pk.NationalCode);

            modelBuilder.Entity<ApplicationUser>().HasIndex(pk => pk.PhoneNumber).IsUnique();
        }
    }
}
