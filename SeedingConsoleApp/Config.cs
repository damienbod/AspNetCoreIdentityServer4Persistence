using AspNetCoreIdentityServer4Persistence.ConfigurationStore;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace SeedingConsoleApp
{
    public class Config
    {
        public static IEnumerable<ClientEntity> GetClients()
        {
            List<ClientEntity> clients = new List<ClientEntity>();
            foreach(var client in GetClientsInternal())
            {
                var clientEntity = new ClientEntity
                {
                    Client = client
                };
                clientEntity.AddDataToEntity();
                clients.Add(clientEntity);
            }

            return clients;
        }

        public static IEnumerable<IdentityResourceEntity> GetIdentityResources()
        {
            List<IdentityResourceEntity> identityResources = new List<IdentityResourceEntity>();
            foreach (var identityResource in GetIdentityResourcesInternal())
            {
                var identityResourceEntity = new IdentityResourceEntity
                {
                    IdentityResource = identityResource
                };
                identityResourceEntity.AddDataToEntity();
                identityResources.Add(identityResourceEntity);
            }

            return identityResources;
        }

        public static IEnumerable<ApiResourceEntity> GetApiResources()
        {
            List<ApiResourceEntity> apiResources = new List<ApiResourceEntity>();
            foreach (var apiResource in GetApiResourcesInternal())
            {
                var apiResourceEntity = new ApiResourceEntity
                {
                    ApiResource = apiResource
                };
                apiResourceEntity.AddDataToEntity();
                apiResources.Add(apiResourceEntity);
            }

            return apiResources;
        }

        private static IEnumerable<IdentityResource> GetIdentityResourcesInternal()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(), 
                new IdentityResource("dataeventrecordsscope",new []{ "role", "admin", "user", "dataEventRecords", "dataEventRecords.admin" , "dataEventRecords.user" } ),
                new IdentityResource("securedfilesscope",new []{ "role", "admin", "user", "securedFiles", "securedFiles.admin", "securedFiles.user"} )
            };
        }

        private static IEnumerable<ApiResource> GetApiResourcesInternal()
        {
            return new List<ApiResource>
            {
                new ApiResource("dataEventRecords")
                {
                    ApiSecrets =
                    {
                        new Secret("dataEventRecordsSecret".Sha256())
                    },
                    Scopes =
                    {
                        new Scope
                        {
                            Name = "dataeventrecordsscope",
                            DisplayName = "Scope for the dataEventRecords ApiResource"
                        }
                    },
                    UserClaims = { "role", "admin", "user", "dataEventRecords", "dataEventRecords.admin", "dataEventRecords.user" }
                },
                new ApiResource("securedFiles")
                {
                    ApiSecrets =
                    {
                        new Secret("securedFilesSecret".Sha256())
                    },
                    Scopes =
                    {
                        new Scope
                        {
                            Name = "securedfilesscope",
                            DisplayName = "Scope for the securedFiles ApiResource"
                        }
                    },
                    UserClaims = { "role", "admin", "user", "securedFiles", "securedFiles.admin", "securedFiles.user" }
                }
            };
        }

        // clients want to access resources (aka scopes)
        private static IEnumerable<Client> GetClientsInternal()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientName = "angularjsclient",
                    ClientId = "angularjsclient",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>
                    {
                        "https://localhost:44376/authorized"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:44346/unauthorized.html"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:44346"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "email",
                        "profile",
                        "dataEventRecords",
                        "dataeventrecordsscope",
                        "securedFiles",
                        "securedfilesscope",
                    }
                },
                new Client
                {
                    ClientName = "angularclient",
                    ClientId = "angularclient",
                    AccessTokenType = AccessTokenType.Reference,
                    AccessTokenLifetime = 330,// 330 seconds, default 60 minutes
                    IdentityTokenLifetime = 300,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>
                    {
                        "https://localhost:44311"

                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:44311/unauthorized",
                        "https://localhost:44311"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:44311",
                        "http://localhost:44311"
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
                },
                new Client
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
}
            };
        }
    }
}