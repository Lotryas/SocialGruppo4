using Microsoft.AspNetCore.Mvc;
using SocialGruppo4.Models.Post;
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

            ViewBag.Posts = DAOPost.GetInstance().MyLatest(utente.Id);

            return View(utente);
        }
    }
}
