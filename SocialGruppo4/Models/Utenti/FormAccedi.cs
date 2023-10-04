using System.ComponentModel.DataAnnotations;

namespace SocialGruppo4.Models.Utenti
{
    public class FormAccedi
    {
        [Required(ErrorMessage = "Il campo Email è richiesto")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Il campo Password è richiesto")]
        public string? Password { get; set; }
    }
}