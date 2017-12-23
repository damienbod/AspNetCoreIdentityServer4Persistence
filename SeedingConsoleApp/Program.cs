using AspNetCoreIdentityServer4Persistence.ConfigurationStore;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Adding a clint to the DB");

            ClientEntity clientEntity = new ClientEntity();
            clientEntity.Client = new Client
            {
                ClientName = "angularclientidtokenonly",
                ClientId = "angularclientidtokenonly",
                AccessTokenType = AccessTokenType.Reference,
                AccessTokenLifetime = 360,// 120 seconds, default 60 minutes
                IdentityTokenLifetime = 300,
                AllowedGrantTypes = GrantTypes.Implicit,
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowAccessTokensViaBrowser = true,
                RedirectUris = new List<string>
                    {
                        "https://localhost:44372"

                    },
                PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:44372/Unauthorized"
                    },
                AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:44372",
                        "http://localhost:44372"
                    },
                AllowedScopes = new List<string>
                    {
                        "openid",
                        "dataEventRecords",
                        "dataeventrecordsscope",
                        "securedFiles",
                        "securedfilesscope",
                        "role",
                        "profile",
                        "email"
                    }
            };
            clientEntity.AddClientDataToEntity();
            clientEntity.MapClientDataFromEntity();

            Console.WriteLine(clientEntity.ClientData);
            Console.WriteLine(clientEntity.Client.ClientName);

            try
            {
                var configurationStoreConnection = "Data Source=C:\\git\\AspNetCoreIdentityServer4Persistence\\AspNetCoreIdentityServer4Persistence\\ConfigurationStore\\identityserver4configuration.sqlite";
                var optionsBuilder = new DbContextOptionsBuilder<ConfigurationStoreContext>();
                optionsBuilder.UseSqlite(configurationStoreConnection);

                using (var configurationStoreContext = new ConfigurationStoreContext(optionsBuilder.Options))
                {
                    configurationStoreContext.Add(clientEntity);
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
