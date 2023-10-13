 using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SocialGruppo4.Models;
using SocialGruppo4.Models.Likes;
using SocialGruppo4.Models.Post;
using SocialGruppo4.Models.Utenti;
using Utility;

namespace SocialGruppo4.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHostEnvironment _env;

    public HomeController(ILogger<HomeController> logger, IHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public IActionResult Index()
    {
        Entity? utente = (Entity?)HttpContext.Items["User"];
        ViewBag.Likes = new List<int>();

        if (utente is not null)
        {
            ViewBag.Utenti = DAOUtenti.GetInstance().LatestToFollow(limit: 6, idUtente: utente.Id);
            ViewBag.Likes = DAOLikes.GetInstance().ReadUserLikedPosts(utente.Id);
            ViewBag.Posts = DAOPost.GetInstance().LatestFollowing(utente.Id);
        }
        else
        {
            ViewBag.Posts = DAOPost.GetInstance().Latest();
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreaPost()
    {
        Utente? utente = (Utente?)HttpContext.Items["User"];
        if (utente is null)
        {
            TempData["FlashMessage"] = new Dictionary<string, string>()
            {
                {"Status", "error"},
                {"Message", "Devi accedere con il tuo account prima di poter pubblicare un post."}
            };
            return RedirectToAction("Index");
        }

        IFormFile? imageFile = Request.Form.Files["imageFile"];
        if (imageFile is not null)
        {
            string path = Path.Combine(_env.ContentRootPath, "wwwroot", "images", "posts", imageFile.FileName);
            using var fileStream = new FileStream(path, FileMode.Create);
            imageFile.CopyTo(fileStream);
        }

        Post post = new()
        {
            IdUtente = utente.Id,
            IdPadre = null,
            Contenuto = Request.Form["contenuto"],
            DataEora = DateTime.Now,
            MiPiace = 0,
            Immagine = imageFile?.FileName
        };

        bool success = DAOPost.GetInstance().Insert(post);
        if (!success)
        {
            TempData["FlashMessage"] = new Dictionary<string, string>()
            {
                {"Status", "error"},
                {"Message", "Qualcosa è andato storto. Riprova più tardi."}
            };
            return RedirectToAction("Index");
        }

        TempData["FlashMessage"] = new Dictionary<string, string>()
        {
            {"Status", "success"},
            {"Message", "Il tuo post è stato pubblicato con successo!"}
        };
        return RedirectToAction("Index");
    }

    [HttpPost("posts/{id:int}/like")]
    public IActionResult ToggleLike(int id)
    {
        Entity? utente = (Entity?)HttpContext.Items["User"];

        if (utente is null)
        {
            return RedirectToAction("Index", "Auth");
        }

        Like like = new()
        {
            IdUtente = utente.Id,
            IdPost = id
        };

        bool success;
        List<int> userLikes = DAOLikes.GetInstance().ReadUserLikedPosts(utente.Id);

        if (userLikes.Contains(id))
        {
            success = DAOLikes.GetInstance().Delete(like);
        }
        else
        {
            success = DAOLikes.GetInstance().Insert(like);
        }

        if (!success)
        {
            TempData["FlashMessage"] = new Dictionary<string, string>()
            {
                {"Status", "error"},
                {"Message", "Qualcosa è andato storto. Riprova più tardi."}
            };
            return RedirectToAction("Index");
        }


        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
