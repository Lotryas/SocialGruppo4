using Utility;

namespace SocialGruppo4.Models.Followers
{
    public class DAOFollower : IDAO
    {
        private readonly Database db;

        private static DAOFollower instance = null!;

        private DAOFollower()
        {
            db = new Database(Config.ConnectionString.Value);
        }

        public static DAOFollower getInstance()
        {
            if (instance == null)
            {
                instance = new DAOFollower();
            }

            return instance;
        }

        public bool Delete(int id)
        {
            return db.Update($"delete from Followers where id = {id}");
        }

        public Entity Find(int id)
        {
            foreach (Entity e in Read())
            {
                if (e.Id == id)
                {
                    return e;
                }
            }

            return null!;
        }

        public bool Insert(Entity e)
        {
            return db.Update(
                            $"insert into Followers " +
                            $"(idUtente, idFollower) " +
                            $"values " +
                            $"('{((Follower)e).IdUtente}','{((Follower)e).IdFollower}')"
                            );
        }

        public List<Entity> Read()
        {
            List<Entity> ris = new List<Entity>();

            List<Dictionary<string, string>> righe = db.Read("select * from Followers");

            foreach (Dictionary<string, string> riga in righe)
            {
                Follower f = new Follower();

                f.FromDictionary(riga);

                ris.Add(f);
            }

            return ris;
        }

        public bool Update(Entity e)
        {
            return db.Update(
                            $"update Followers set " +
                            $"idUtente = '{((Follower)e).IdUtente}', " +
                            $"idFollower = '{((Follower)e).IdFollower}' "
                            );
        }
    }
}
