using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace shopping_website.Models
{
    public class Product
    {
        [JsonRequired]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonRequired]
        public decimal Price { get; set; }
        [JsonRequired]
        public int Stock { get; set; }
    }
}