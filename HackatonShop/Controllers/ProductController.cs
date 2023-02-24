using System.Collections.Generic;
using System.Linq;
using HackatonShop.DataAccessLayer;
using HackatonShop.DomainModels;
using Microsoft.AspNetCore.Mvc;

namespace HackatonShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly UnitOfWork _uow;

        public ProductController(UnitOfWork uow)
        {
            _uow = uow;
        }
    
        [HttpGet]
        public List<Product> Index()
        {
            return _uow.Products.ToList();
        }
    }
}