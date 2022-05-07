using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Toro.Domain.Entity;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Toro.Repository.Context {
    public class ToroContext : IdentityDbContext, IToroContext {
        private static string connectionString;
        public ToroContext(DbContextOptions<ToroContext> options, IConfiguration configuration)
                : base(options) {
            if (connectionString is null) {
                // Para acesso do migrations à connectionstring
                connectionString = configuration.GetSection("ConnectionStrings:sqlServerConnection").Value;
            }
        }

        public DbSet<Asset> Asset { get; set; }
        public DbSet<Investor> Investor { get; set; }
        public DbSet<Patrimony> Patrimony { get; set; }
        public DbSet<AssetXPatrimony> AssetXPatrimony { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToroContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(connectionString);
            }

            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        }

        public async Task<int> SaveChangesAsync() {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("RegisteringDate") != null)) {
                if (entry.State == EntityState.Added) {
                    entry.Property("RegisteringDate").CurrentValue = DateTime.Now;
                    entry.Property("LastUpdate").CurrentValue = DateTime.Now;
                } else if (entry.State == EntityState.Modified) {
                    entry.Property("RegisteringDate").IsModified = false;
                    entry.Property("Id").IsModified = false;
                    entry.Property("LastUpdate").CurrentValue = DateTime.Now;
                }
            }
            return await base.SaveChangesAsync();
        }
    }
}
