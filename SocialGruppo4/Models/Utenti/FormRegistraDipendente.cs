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
        [RegularExpression(
            "^(([+]|00)39)?((3[1-6][0-9]))(\\d{7})$",
            ErrorMessage = "Numero di Cellulare non valido"
        )]
        public string? Numero { get; set; } = "";

        public string? Residenza { get; set; } = "";

        [Required(ErrorMessage = "Il campo Codice Fiscale è richiesto")]
        [RegularExpression(
            "^[A-Za-z]{6}[0-9]{2}[A-Za-z]{1}[0-9]{2}[A-Za-z]{1}[0-9]{3}[A-Za-z]{1}$",
            ErrorMessage = "Il Codice Fiscale non è valido"
        )]
        public string? CodiceFiscale { get; set; } = "";
    }
}