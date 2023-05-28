using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Tanner.Core.DataAccess.Models;
using Tanner.RelationalDataAccess;

namespace Tanner.Core.DataAccess
{
    public class CoreContext : TannerContext<CoreContext>
    {
        public DbSet<DocumentQuotation> Quotation { get; set; }

        public DbSet<Operation> Operation { get; set; }

        public DbSet<Person> Person { get; set; }

        public DbSet<Account> Account { get; set; }

        public DbSet<CurrentAccount> CurrentAccount { get; set; }

        public DbSet<Address> Address { get; set; }

        public DbSet<Commune> Commune { get; set; }

        public CoreContext(DbContextOptions<CoreContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Address>()
                .Property(t => t.ID)
                .ValueGeneratedOnAdd()
                .UseSqlServerIdentityColumn();
        }
    }
}
