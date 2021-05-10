using System.Text.Json.Serialization;
using SQLite;

namespace MobileDevelopment.Models
{
    [Table("Hits")]
    public class Hit
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        
        [JsonPropertyName("previewURL")]
        public string PreviewUrl { get; set; }
    }
}