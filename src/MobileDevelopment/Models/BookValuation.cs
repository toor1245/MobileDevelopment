using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MobileDevelopment.Models
{
    public class BookValuation
    {
        [JsonPropertyName("books")]
        public List<Book> Books { get; set; }
    }
}