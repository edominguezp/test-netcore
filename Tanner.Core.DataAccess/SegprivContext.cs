using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tanner.Core.DataAccess.Models.Segpriv;
using Tanner.RelationalDataAccess;

namespace Tanner.Core.DataAccess
{
    public class SegprivContext : TannerContext<SegprivContext>
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public SegprivContext(DbContextOptions<SegprivContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<UserProfile>()
                .HasKey(t => new { t.ProfileId, t.LoginName });
        }
    }
}
