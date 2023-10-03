using Microsoft.AspNetCore.Mvc;
using SocialGruppo4.Models.Utenti;

namespace SocialGruppo4.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet("accedi")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("accedi")]
        public IActionResult OnLogin()
        {
            string email = Request.Form["email"];
            string plainPassword = Request.Form["password"];

            Utente? utente = (Utente?)DAOUtenti.GetInstance().Find(email, plainPassword);

            if (utente is null)
            {
                TempData.Add("Message", "La combinazione di Email e Password non è stata riconosciuta.");
                return RedirectToAction(nameof(Index));
            }

            CookieOptions cookieOpts = new()
            {
                Expires = DateTime.Now.AddYears(1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax
            };

            Response.Cookies.Append("auth", utente.Id.ToString(), cookieOpts);

            return RedirectToAction(nameof(Index));
        }
    }
}