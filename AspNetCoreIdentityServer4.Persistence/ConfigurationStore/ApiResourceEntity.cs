using IdentityServer4.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentityServer4Persistence.ConfigurationStore
{
    public class ApiResourceEntity
    {
        public string ApiResourceData { get; set; }

        [Key]
        public string ApiResourceName { get; set; }

        [NotMapped]
        public ApiResource ApiResource { get; set; }

        public void AddDataToEntity()
        {
            ApiResourceData = JsonConvert.SerializeObject(ApiResource);
            ApiResourceName = ApiResource.Name;
        }

        public void MapDataFromEntity()
        {
            ApiResource = JsonConvert.DeserializeObject<ApiResource>(ApiResourceData);
            ApiResourceName = ApiResource.Name;
        }
    }
}
