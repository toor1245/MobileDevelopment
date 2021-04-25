using System.Reflection;
using System.Text.Json.Serialization;
using MobileDevelopment.Extensions;
using MobileDevelopment.Views;
using Xamarin.Forms;

namespace MobileDevelopment.Models
{
    public class Book
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
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
        public ImageSource ImageSource => ImageSource.FromResource(string.Concat(Constants.RESOURCES, Image), typeof(BooksPage).GetTypeInfo().Assembly);
    }
}