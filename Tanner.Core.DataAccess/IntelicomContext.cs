using Microsoft.EntityFrameworkCore;
using Tanner.Core.DataAccess.Models;
using Tanner.RelationalDataAccess;

namespace Tanner.Core.DataAccess
{
    public class IntelicomContext : TannerContext<IntelicomContext>
    {
        public DbSet<ClientProfile> ClientProfile { get; set; }

        public IntelicomContext(DbContextOptions<IntelicomContext> options) : base(options)
        {
        }
    }
}
