using Utility;

namespace SocialGruppo4.Models.Likes
{
    public class Like : Entity
    {
        public int IdUtente { get; set; }
        public int IdPost { get; set; }

        public Like() { }

        public Like(int id, int idUtente, int idPost) : base(id)
        {
            IdUtente = idUtente;
            IdPost = idPost;
        }
    }
}