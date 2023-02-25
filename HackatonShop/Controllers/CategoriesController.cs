using System.Linq;
using HackatonShop.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HackatonShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly UnitOfWork _uow;

        public CategoriesController(UnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public ActionResult Index(int? categoryId)
        {
            if (categoryId is null) return Ok(_uow.Products.ToList());
           return Ok(_uow.Categories.Where(x=>x.Id == categoryId).Include(x=>x.Products).ToList());
        }
    }
}