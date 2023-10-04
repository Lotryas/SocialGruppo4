using System.ComponentModel.DataAnnotations;

namespace SocialGruppo4.Models.Utenti
{
    public class FormRegistraDipendente
    {
        [Required(ErrorMessage = "Il campo Nominativo è richiesto")]
        public string? Nominativo { get; set; }

        [Required(ErrorMessage = "Il campo Email è richiesto")]
        [EmailAddress]
        public string? Email { get; set; } = "";

        [Required(ErrorMessage = "Il campo Cellulare è richiesto")]
        [Phone]
        public string? Numero { get; set; } = "";

        public string? Residenza { get; set; } = "";

        [Required(ErrorMessage = "Il campo Codice Fiscale è richiesto")]
        public string? CodiceFiscale { get; set; } = "";
    }
}