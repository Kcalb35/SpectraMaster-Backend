using System.Text.Json.Serialization;

namespace SpectraMaster.Models
{
    public class TokenManagement
    {
        [JsonPropertyName("secret")]
        public string Secret{ get; set; }
        
        [JsonPropertyName("issuer")]
        public string Issuer { get; set; }
        [JsonPropertyName("audience")]
        public string Audience { get; set; }
    }
}