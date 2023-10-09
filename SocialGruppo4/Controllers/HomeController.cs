using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SocialGruppo4.Models;
using SocialGruppo4.Models.Utenti;
using Utility;

namespace SocialGruppo4.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
