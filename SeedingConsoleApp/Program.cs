using AspNetCoreIdentityServer4Persistence.ConfigurationStore;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace SeedingConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var currentDirectory = Directory.GetCurrentDirectory();

                var configuration = new ConfigurationBuilder()
                    .AddJsonFile($"{currentDirectory}\\..\\AspNetCoreIdentityServer4\\appsettings.json")
                    .Build();

                var configurationStoreConnection = configuration.GetConnectionString("ConfigurationStoreConnection");

                var optionsBuilder = new DbContextOptionsBuilder<ConfigurationStoreContext>();
                optionsBuilder.UseSqlite(configurationStoreConnection);

                using (var configurationStoreContext = new ConfigurationStoreContext(optionsBuilder.Options))
                {
                    configurationStoreContext.AddRange(Config.GetClients());
                    configurationStoreContext.AddRange(Config.GetIdentityResources());
                    configurationStoreContext.AddRange(Config.GetApiResources());
                    configurationStoreContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
