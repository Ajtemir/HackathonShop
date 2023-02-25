using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HackatonShop.DomainModels
{
    public class Product
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        [JsonPropertyName("title")]
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonPropertyName("parentId")]
        public int CategoryId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<Comment> Comments { get; set; }
         
        
    }
}