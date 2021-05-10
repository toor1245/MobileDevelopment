using System.Text.Json.Serialization;
using SQLite;

namespace MobileDevelopment.Models
{
    [Table("BookDetails")]
    public class BookDetail
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        
        [JsonPropertyName("title")] 
        public string Title { get; set; }
        
        [JsonPropertyName("isbn13")]
        public string Isbn13 { get; set; }

        [JsonPropertyName("subtitle")] 
        public string SubTitle { get; set; }
        [JsonPropertyName("price")] 
        public string Price { get; set; }
        
        [JsonPropertyName("authors")] 
        public string Authors { get; set; }
        
        [JsonPropertyName("publisher")] 
        public string Publisher { get; set; }

        [JsonPropertyName("pages")] 
        public string Pages { get; set; }

        [JsonPropertyName("year")] 
        public string Year { get; set; }

        [JsonPropertyName("rating")] 
        public string Rating { get; set; }

        [JsonPropertyName("desc")] 
        public string Description { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }
    }
}