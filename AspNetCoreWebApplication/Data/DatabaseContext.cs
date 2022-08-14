using AspNetCoreWebApplication.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApplication.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=(localdb)\MSSQLLocalDB; Database=AspNetCoreWebApplication; integrated security=true; MultipleActiveResultSets=True");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = 1,
                    IsActive = true,
                    IsAdmin = true,
                    UserName = "Admin",
                    Password = "123"
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
