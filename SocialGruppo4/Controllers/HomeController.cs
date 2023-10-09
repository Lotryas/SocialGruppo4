using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SocialGruppo4.Models;
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
        if (utente is not null)
        {
            ViewBag.Utenti = DAOUtenti.GetInstance().LatestToFollow(limit: 6, idUtente: utente.Id);
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult CreaPost()
    {
        IFormFile? imageFile = Request.Form.Files["imageFile"];
        if (imageFile is not null)
        {
            string path = Path.Combine(_env.ContentRootPath, "wwwroot", "images", "posts", imageFile.FileName);
            using var fileStream = new FileStream(path, FileMode.Create);
            imageFile.CopyTo(fileStream);
        }

        Console.WriteLine(Request.Form["content"]);

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
