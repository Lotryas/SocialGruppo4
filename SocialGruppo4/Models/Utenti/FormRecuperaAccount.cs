using System.ComponentModel.DataAnnotations;

namespace SocialGruppo4.Models.Utenti
{
    public class FormRecuperaAccount
    {
        [Required(ErrorMessage = "Il campo Email è richiesto")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Il campo Password è richiesto")]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "La Password deve essere di 8 o massimo 32 caratteri")]
        [Compare("ConfermaPassword", ErrorMessage = "La Password e Conferma Password non coincidono")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Il campo Conferma Password è richiesto")]
        [Compare("ConfermaPassword", ErrorMessage = "La Password e Conferma Password non coincidono")]
        public string? ConfermaPassword { get; set; }
    }
}