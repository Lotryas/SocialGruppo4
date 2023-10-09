using Microsoft.AspNetCore.Mvc;
using SocialGruppo4.Models.Followers;
using SocialGruppo4.Models.Utenti;
using Utility;

namespace SocialGruppo4.Controllers
{
    public class UtentiController : Controller
    {
        public IActionResult ElencoUtenti()
        {
            List<Entity> e = DAOUtenti.GetInstance().Read();

            List<Utente> utenti = new List<Utente>();
            foreach (var entitaItem in e)
            {
                Utente utente = new Utente
                {
                    Nominativo = ((Utente)entitaItem).Nominativo,
                    Email = ((Utente)entitaItem).Email
                };
                utenti.Add(utente);
            }

            return View(utenti);
        }

        [HttpPost("utenti/follow/{id:int}")]
        public IActionResult Follow(int id)
        {
            Entity? utente = (Entity?)HttpContext.Items["User"];

            if (utente is null)
            {
                return RedirectToAction("Index", "Auth");
            }

            Utente? uFollow = (Utente?)DAOUtenti.GetInstance().Find(id);
            if (uFollow is null)
            {
                TempData["FlashMessage"] = new Dictionary<string, string>{
                    {"Status", "error"},
                    {"Message", "Utente da seguire non trovato."}
                };

                return RedirectToAction("Index", "Home");
            }

            Follower follower = new()
            {
                IdUtente = utente.Id,
                IdFollower = id,
            };

            bool success = DAOFollower.getInstance().Insert(follower);
            if (!success)
            {
                TempData["FlashMessage"] = new Dictionary<string, string>{
                    {"Status", "error"},
                    {"Message", "Stai già seguendo questo utente."}
                };

                return RedirectToAction("Index", "Home");
            }

            TempData["FlashMessage"] = new Dictionary<string, string>{
                {"Status", "success"},
                {"Message", $"Fatto! Da ora segui {uFollow?.Nominativo}."}
            };

            return RedirectToAction("Index", "Home");
        }
    }
}
