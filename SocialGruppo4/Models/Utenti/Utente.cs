using Utility;

namespace SocialGruppo4.Models.Utenti
{
    public class Utente : Entity
    {
        public string Nominativo { get; set; } = "";
        public bool Amministratore { get; set; }
        public string Email { get; set; } = "";
        public string Numero { get; set; } = "";
        public string Residenza { get; set; } = "";
        public string CodiceFiscale { get; set; } = "";
        public string PasswordHash { get; set; } = "";

        public Utente() { }

        public Utente(
            int id, string nominativo, bool amministratore, string email, string numero,
            string residenza, string codiceFiscale, string passwordHash
        ) : base(id)
        {
            Nominativo = nominativo;
            Amministratore = amministratore;
            Email = email;
            Numero = numero;
            Residenza = residenza;
            CodiceFiscale = codiceFiscale;
            PasswordHash = passwordHash;
        }
    }
}