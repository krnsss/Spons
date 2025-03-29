using System.Text.Json.Serialization;

namespace egorDipl.Data.Models
{
    public class TokenDto
    {
        public TokenDto()
        {

        }

        public TokenDto(string accessToken)
        {
            AccessToken = accessToken;
        }

        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }
    }
}
