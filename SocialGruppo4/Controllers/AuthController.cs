using System.Data.Common;
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
                TempData["FlashMessage"] = new Dictionary<string, string>
                {
                    {"Status", "error"},
                    {"Message", "Hai già effettuato l'accesso"}
                };
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost("accedi")]
        public IActionResult Index(FormAccedi form)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Utente? utente = (Utente?)DAOUtenti.GetInstance().Find(form.Email!, form.Password!);

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
            {
                TempData["FlashMessage"] = new Dictionary<string, string>
                {
                    {"Status", "error"},
                    {"Message", "Accedi al tuo account per poter visualizzare la pagina."}
                };
                return RedirectToAction("Index");
            }
            else if (!utente.Amministratore)
            {
                TempData["FlashMessage"] = new Dictionary<string, string>
                {
                    {"Status", "error"},
                    {"Message", "Non hai i giusti permessi per poter visualizzare la pagina."}
                };
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost("registra-dipendente")]
        public IActionResult RegistraDipendente(FormRegistraDipendente form)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Utente utente = new()
            {
                Nominativo = form.Nominativo!,
                Email = form.Email!,
                CodiceFiscale = form.CodiceFiscale!,
                Numero = form.Numero!,
                Residenza = form.Residenza ?? "",
                Amministratore = false,
                PasswordHash = Utente.GetRandomPassword(12)
            };

            var success = DAOUtenti.GetInstance().Insert(utente);
            if (!success)
            {
                TempData["FlashMessage"] = new Dictionary<string, string>
                {
                    {"Status", "error"},
                    {"Message", "ERRORE: è stato già registrato un dipendente con questi dati."}
                };
                return View();
            }

            TempData["FlashMessage"] = new Dictionary<string, string>
            {
                {"Status", "success"},
                {"Message", $"Il dipendente {utente.Nominativo} è stato registrato con successo!"}
            };
            return RedirectToAction("Index", "Home");
        }


        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("auth");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("recupera-account")]
        public IActionResult RecuperaAccount()
        {
            return View();
        }

        [HttpPost("recupera-account")]
        public IActionResult RecuperaAccount(FormRecuperaAccount form)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Utente? utente = (Utente?)DAOUtenti.GetInstance().Find(form.Email!);

            if (utente is null)
            {
                TempData.Add("Message", $"Utente con email {form.Email!} non trovato.");
                return RedirectToAction("RecuperaAccount");
            }

            DAOUtenti.GetInstance().UpdatePassword(form.Email!, form.Password!);

            return RedirectToAction("Index");
        }
    }
}