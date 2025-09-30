using data_analyse.Data;
using Microsoft.EntityFrameworkCore;

namespace data_analyse.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UploadFile> UploadFiles => Set<UploadFile>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UploadFile>()
                .Property(p => p.Data)
                .HasColumnType("varbinary(max)");
        }
    }
}
