using System.Text.Json.Serialization;

namespace DotnetBoilerplate.Domain.Common
{
    public abstract class BaseDomainEntity
    {
        public int Id { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
