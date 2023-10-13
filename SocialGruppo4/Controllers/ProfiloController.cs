using Microsoft.AspNetCore.Mvc;
using SocialGruppo4.Models.Utenti;

namespace SocialGruppo4.Controllers
{
    public class ProfiloController : Controller
    {
        public IActionResult Index()
        {
            Utente? utente = (Utente?)HttpContext.Items["User"];
            if (utente is null)
            {
                return RedirectToAction("Index", "Auth");
            }

            return View(utente);
        }
    }
}
