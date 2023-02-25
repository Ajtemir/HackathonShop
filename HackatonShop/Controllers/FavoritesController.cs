using System.Linq;
using HackatonShop.DataAccessLayer;
using HackatonShop.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HackatonShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FavoritesController : ControllerBase
    {
        private readonly UnitOfWork _uow;

        public FavoritesController(UnitOfWork uow)
        {
            _uow = uow;
        }

        private const string Key = "favorites";
        private const string Separator = ",";
        
        [HttpGet("[action]")]
        // [Authorize]
        public ActionResult Add(int productId,string email)
        {
            // var name = User.Identity.Name;
            var user = _uow.Users.FirstOrDefault(x => x.Email == email);
            var favorite = new Favorite()
            {
                ProductId = productId,
                UserId = user.Id
            };
            if(_uow.Favorites.FirstOrDefault(x=>x.ProductId == productId && x.UserId == user.Id) is null)_uow.Favorites.Add(favorite);
            _uow.SaveChanges();
            return Ok(favorite);
        }
        
        [HttpGet("[action]")]
        // [Authorize]

        public ActionResult Delete(int productId, string email)
        {
            // var name = User.Identity.Name;
            var user = _uow.Users.FirstOrDefault(x => x.Email == email);
            var favorite = _uow.Favorites.FirstOrDefault(x => x.ProductId == productId && x.UserId == user.Id);
            if(favorite is not null)_uow.Favorites.Remove(favorite);
            _uow.SaveChanges();
            return Ok(favorite);
        }

        [HttpGet("[action]")]
        // [Authorize]
        public ActionResult Get(string email)
        {
            // var name = User.Identity.Name;
            var user = _uow.Users.FirstOrDefault(x => x.Email == email);
            return Ok(_uow.Favorites.Where(x => x.UserId == user.Id)
                .Include(x=>x.Product)
                .Select(x=>x.Product).ToList());
        }
    }
}