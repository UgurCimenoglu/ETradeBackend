using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ETradeBackend.Application.DTOs.Google
{
    public class CreateGoogleJwtRequestDto
    {
        [JsonPropertyName("code")]
        public string code { get; set; }

        [JsonPropertyName("client_id")]
        public string client_id { get; set; }

        [JsonPropertyName("client_secret")]
        public string client_secret { get; set; }

        [JsonPropertyName("redirect_uri")]
        public string redirect_uri { get; set; }

        [JsonPropertyName("grant_type")]
        public string grant_type { get; set; }
    }
}
