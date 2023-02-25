using System.Linq;
using HackatonShop.DataAccessLayer;
using HackatonShop.DomainModels;
using Microsoft.AspNetCore.Mvc;

namespace HackatonShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly UnitOfWork _uow;

        public CommentsController(UnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost]
        public ActionResult Add([FromBody] CommentViewModel model)
        {
            var comment = new Comment
            {
                Text = model.Comment,
                ProductId = model.ProductId,
                UserId = _uow.Users.FirstOrDefault(x => x.Email == model.Email)!.Id
            };
            _uow.Comments.Add(comment);
            _uow.SaveChanges();
            return Ok(comment);
        }
    }

    public class CommentViewModel
    {
        public string Email { get; set; }
        public string Comment { get; set; }
        public int ProductId { get; set; }
    }
}