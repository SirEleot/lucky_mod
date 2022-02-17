using Microsoft.EntityFrameworkCore;
using Server.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Database
{
    class ServerDbContext: DbContext
    {

        public static event Action<ModelBuilder> ModelCreating;
        public DbSet<LuckyAccount> Accounts { get; set; }
        public DbSet<LuckyCharacter> Characters { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(Lucky.LuckySettings.DbConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelCreating?.Invoke(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
