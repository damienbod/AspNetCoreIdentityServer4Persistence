using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AspNetCoreIdentityServer4Persistence.ConfigurationStore
{
    public class ConfigurationStoreContextFactory : IDesignTimeDbContextFactory<ConfigurationStoreContext>
    {
        public ConfigurationStoreContext CreateDbContext(string[] args)
        {
            var deploymentType = 
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentVariableTarget.Machine);

            var currentDirectory = Directory.GetCurrentDirectory();

            var configuration = new ConfigurationBuilder()
                .AddJsonFile($"{currentDirectory}\\appsettings.json")
                .AddJsonFile($"{currentDirectory}\\appsettings.{deploymentType}.json", optional: true)
                .Build();

            var configurationStoreConnection = configuration.GetConnectionString("ConfigurationStoreConnection");
            var optionsBuilder = new DbContextOptionsBuilder<ConfigurationStoreContext>();
            optionsBuilder.UseSqlite(
                configurationStoreConnection,
                    b => b.MigrationsAssembly("AspNetCoreIdentityServer4"));

            return new ConfigurationStoreContext(optionsBuilder.Options);
        }
    }
}