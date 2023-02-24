namespace HackatonShop.DomainModels
{
    public class Order
    {
        public int Id { get; set; }
        public int Pool { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}