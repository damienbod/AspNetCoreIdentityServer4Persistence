using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentityServer4Persistence.ConfigurationStore
{
    public class ClientStore : IClientStore
    {
        private readonly ConfigurationStoreContext _context;
        private readonly ILogger _logger;

        public ClientStore(ConfigurationStoreContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("ClientStore");
        }

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = _context.Clients.First(t => t.ClientId == clientId);
            client.MapDataFromEntity();
            return Task.FromResult(client.Client);
        }
    }
}
