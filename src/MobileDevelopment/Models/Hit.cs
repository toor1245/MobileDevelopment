using System.Text.Json.Serialization;

namespace MobileDevelopment.Models
{
    public class Hit
    {
        [JsonPropertyName("previewURL")]
        public string PreviewUrl { get; set; }
    }
}