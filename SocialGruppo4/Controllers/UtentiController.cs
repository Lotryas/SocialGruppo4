using Microsoft.AspNetCore.Mvc;
using SocialGruppo4.Models.Utenti;
using Utility;

namespace SocialGruppo4.Controllers
{
    public class UtentiController : Controller
    {
        public ActionResult ElencoUtenti()
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


    }
}
