using Microsoft.EntityFrameworkCore;

namespace IllusionPackageDatabase
{
    public sealed class DatabaseContext : DbContext
    {
        public DbSet<PackageModel> Packages { get; set; } = default!;

        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder
            .Entity<PackageModel>()
            .OwnsOne(p => p.Version);

        protected override void OnConfiguring(DbContextOptionsBuilder builder) => builder
            .UseSqlite($"Data Source={System.IO.Path.Combine("..", "..", "..", "..", "Content", "packages.db")}");
    }
}