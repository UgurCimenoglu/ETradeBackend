using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ETradeBackend.Application.DTOs.Facebook
{
    public class FacebookUserAceesTokenValidation
    {
        [JsonPropertyName("data")]
        public Data Data { get; set; }

    }
    public class Data
    {
        [JsonPropertyName("is_valid")]
        public bool IsValid { get; set; }
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
    }
}
