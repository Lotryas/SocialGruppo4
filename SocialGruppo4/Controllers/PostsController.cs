using Microsoft.AspNetCore.Mvc;
using SocialGruppo4.Models.Likes;
using SocialGruppo4.Models.Post;
using SocialGruppo4.Models.Utenti;
using Utility;

namespace SocialGruppo4.Controllers
{
    public class PostsController : Controller
    {
        [HttpGet("Posts/Details/{id}")]
        public IActionResult Details(int id) 
        {
            Console.WriteLine(id);
            Entity? utente = (Entity?)HttpContext.Items["User"];

            if(utente != null) 
                ViewBag.Likes = DAOLikes.GetInstance().ReadUserLikedPosts(utente.Id);

            ViewBag.Commenti = DAOPost.GetInstance().FullPost(id);

            Post temp = (Post)DAOPost.GetInstance().Find(id);


            return View(temp);
        }
    }
}
