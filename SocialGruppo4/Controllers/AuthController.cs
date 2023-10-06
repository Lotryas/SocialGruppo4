using Microsoft.AspNetCore.Mvc;
using SocialGruppo4.Models.Utenti;

namespace SocialGruppo4.Controllers
{
    public class AuthController : Controller
    {
        // DOC: Vista per visualizzare la pagina di Login.
        // Se l'utente ha già effettuato l'accesso viene reindirizzato
        // alla pagina Home.
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

        // DOC: Metodo POST che risponde al submit della form di Login.
        // Riceve come parametro il modello usato nella vista Login e
        // controlla che non ci siano errori di validazioni come dichiarato
        // nel modello FormAccedi.
        // Quando l'utente inserisce l'email e la password corretta, viene
        // salvato il cookie di autenticazione nella Response da restituire.
        // Altrimenti, riceve il messaggio di errore.
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

        // DOC: Vista della pagina di registrazione di un nuovo dipendente.
        // Il metodo fa particolare attenzione a controllare che l'utente
        // sia loggato e che abbia l'autorizzazione per poter inserire nuovi
        // utenti sulla piattaforma.
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

        // DOC: Metodo POST per la registrazione di un nuovo dipendente.
        // Come parametro riceve il modello con le annotazioni di validazione
        // per controllare che i dati della form siano corretti.
        // Se alcuni dati (email, numero o codice fiscale) sono già presenti in
        // database, l'amministrazione riceve un errore.
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


        // DOC: Questo metodo effettua il logout dell'utente tramite
        // l'eliminazione del cookie di autenticazione creato in precedenza.
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("auth");
            return RedirectToAction("Index", "Home");
        }

        // DOC: Vista della pagina del recupero account di un utente.
        // Ovviamente non è usabile una cosa del genere in production,
        // ma per semplicità abbiamo optato per questa soluzione.
        [HttpGet("recupera-account")]
        public IActionResult RecuperaAccount()
        {
            return View();
        }

        // DOC: Metodo POST della form di recupero account.
        // Il modello ricevuto come parametro contiene alcune annotazioni
        // di base per la validazione. Tramite l'inserimento di un email
        // è possibile reimpostare la password, ridando l'accesso
        // all'utente.
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