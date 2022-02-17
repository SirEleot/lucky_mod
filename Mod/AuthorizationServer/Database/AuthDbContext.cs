using AuthorizationServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokenService.Models;

namespace AuthorizationServer.Database
{
    public class AuthDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TokenModel> Tokens { get; set; }
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TokenModel>().Ignore(t => t.Sign);
        }
    }
}
