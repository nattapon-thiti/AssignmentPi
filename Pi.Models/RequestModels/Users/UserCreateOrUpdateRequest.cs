using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Pi.Models.RequestModels.Users
{
    public class UserCreateOrUpdateRequest
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        //[Required]
        [JsonPropertyName("name")]
        public string GivenName { get; set; } = string.Empty;
        //[Required]
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
    }
}
