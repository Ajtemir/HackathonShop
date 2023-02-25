using System.ComponentModel.DataAnnotations.Schema;

namespace HackatonShop.DomainModels
{
    public class Favorite
    {
        public int Id { get; set; }
        public int Pool { get; set; } = 0;
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}