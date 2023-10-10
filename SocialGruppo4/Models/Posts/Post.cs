using Utility;

namespace SocialGruppo4.Models.Post
{
    public class Post : Entity
    {
        public int IdUtente { get; set; }
        public int IdPadre { get; set; }
        public string Contenuto { get; set; } = "";
        public DateTime DataEora { get; set; }
        public int MiPiace { get; set; }
        public string? Immagine { get; set; }

        public Post() { }

        public Post(
            int id, int idUtente, int idPadre, string contenuto,
            DateTime dataEora, int miPiace, string immagine
        ) : base(id)
        {
            IdUtente = idUtente;
            IdPadre = idPadre;
            Contenuto = contenuto;
            DataEora = dataEora;
            MiPiace = miPiace;
            Immagine = immagine;
        }
    }
}
