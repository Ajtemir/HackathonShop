using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HackatonShop.DomainModels
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}