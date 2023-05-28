using Microsoft.EntityFrameworkCore;
using Tanner.Core.DataAccess.Models;
using Tanner.RelationalDataAccess;

namespace Tanner.Core.DataAccess
{
    public class CoreFileContext : TannerContext<CoreFileContext>
    {
        public DbSet<File> File { get; set; }

        public DbSet<FileLine> FileLine { get; set; }

        public DbSet<OperationFile> OperationFile { get; set; }

        public CoreFileContext(DbContextOptions<CoreFileContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<OperationFile>()
                .HasKey(t => new { t.FileID, t.OperationID });
        }
    }
}
