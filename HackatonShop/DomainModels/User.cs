using System.Collections.Generic;

namespace HackatonShop.DomainModels
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string  Role { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
    }
}