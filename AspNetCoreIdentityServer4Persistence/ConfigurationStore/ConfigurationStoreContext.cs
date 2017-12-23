using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentityServer4Persistence.ConfigurationStore
{
    public class ConfigurationStoreContext : DbContext
    {
        public ConfigurationStoreContext(DbContextOptions<ConfigurationStoreContext> options) : base(options)
        { }

        public DbSet<ClientEntity> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ClientEntity>().HasKey(m => m.ClientId);
            base.OnModelCreating(builder);
        }
    }
}
