using System.Text.Json.Serialization;
using SQLite;

namespace MobileDevelopment.Models
{
    [Table("Books")]
    public class Book
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        
        [JsonPropertyName("id")]
        public string BookId { get; set; }
        
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [JsonPropertyName("subtitle")]
        public string SubTitle { get; set; }
        
        [JsonPropertyName("isbn13")]
        public string Isbn13 { get; set; }
        
        [JsonPropertyName("price")]
        public string Price { get; set; }
        
        [JsonPropertyName("image")]
        public string Image { get; set; }
    }
}