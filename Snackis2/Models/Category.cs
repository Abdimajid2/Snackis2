using System.Text.Json.Serialization;

namespace Snackis2.Models
{
    public class Category
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("kategoryname")]
        public string? KategoryName { get; set; }

    }
}
