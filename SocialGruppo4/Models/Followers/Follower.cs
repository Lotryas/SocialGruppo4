using Utility;

namespace SocialGruppo4.Models.Followers
{
    public class Follower : Entity
    {
        public int IdUtente { get; set; }
        public int IdFollower { get; set; }

        public Follower() { }
        public Follower(int id, int idUtente, int idFollower) : base(id)
        {
            IdUtente = idUtente;
            IdFollower = idFollower;
        }
    }
}
