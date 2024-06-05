using System.Text.Json.Serialization;

namespace DotnetBoilerplate.Domain.Payloads
{
    public class TokenPayload
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Access { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Refresh { get; set; }
    }
}
