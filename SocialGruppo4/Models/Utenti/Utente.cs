using System.Text;
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

        public static string GetRandomPassword(int length)
        {
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random rnd = new();
            string password = "";

            for (int i = 0; i < length; i++)
            {
                int j = rnd.Next(chars.Length);
                password += chars[j];
            }

            return password;
        }
    }
}