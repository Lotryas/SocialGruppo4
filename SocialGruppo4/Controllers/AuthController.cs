using Microsoft.AspNetCore.Mvc;
using SocialGruppo4.Models.Utenti;

namespace SocialGruppo4.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet("accedi")]
        public IActionResult Index()
        {
            if (HttpContext.Items["User"] is not null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost("accedi")]
        public IActionResult Index(FormAccedi formAccedi)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Utente? utente = (Utente?)DAOUtenti.GetInstance().Find(formAccedi.Email!, formAccedi.Password!);

            if (utente is null)
            {
                TempData.Add("Message", "La combinazione di Email e Password non è stata riconosciuta.");
                return RedirectToAction("Index");
            }

            CookieOptions cookieOpts = new()
            {
                Expires = DateTime.Now.AddYears(1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax
            };

            Response.Cookies.Append("auth", utente.Id.ToString(), cookieOpts);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("registra-dipendente")]
        public IActionResult RegistraDipendente()
        {
            var utente = (Utente?)HttpContext.Items["User"];

            if (utente is null)
                return RedirectToAction("Index");
            else if (!utente.Amministratore)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost("registra-dipendente")]
        public IActionResult OnRegister()
        {
            return null;
        }
    }
}