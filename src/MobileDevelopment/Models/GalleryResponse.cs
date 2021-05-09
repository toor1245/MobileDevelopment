using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MobileDevelopment.Models
{
    public class GalleryResponse
    {
        [JsonPropertyName("hits")]
        public List<Hit> Hits { get; set; }
    }
}