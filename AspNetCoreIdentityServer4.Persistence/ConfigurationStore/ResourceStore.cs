using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentityServer4Persistence.ConfigurationStore
{
    public class ResourceStore : IResourceStore
    {
        private readonly ConfigurationStoreContext _context;
        private readonly ILogger _logger;

        public ResourceStore(ConfigurationStoreContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("ResourceStore");
        }

        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            var apiResource = _context.ApiResources.First(t => t.ApiResourceName == name);
            apiResource.MapDataFromEntity();
            return Task.FromResult(apiResource.ApiResource);
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null) throw new ArgumentNullException(nameof(scopeNames));

            var apiResources = (from item in _context.ApiResources
                                     select item.ApiResource).ToList();

            var apis = from i in apiResources
                             where scopeNames.Contains(i.Name)
                             select i;

            return Task.FromResult(apis);
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null) throw new ArgumentNullException(nameof(scopeNames));

            var identityResources = (from item in _context.IdentityResources
                                     select item.IdentityResource).ToList();

            var identities = from i in identityResources
                             where scopeNames.Contains(i.Name)
                           select i;

            return Task.FromResult(identities);
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            var apiResources = (from item in _context.ApiResources
                             select item.ApiResource).ToList();

            var identityResources = (from item in _context.IdentityResources
                                select item.IdentityResource).ToList();

            var result = new Resources(identityResources, apiResources);
            return Task.FromResult(result);
        }
    }
}
