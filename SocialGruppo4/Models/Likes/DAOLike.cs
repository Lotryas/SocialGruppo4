using Utility;

namespace SocialGruppo4.Models.Likes
{
    public class DAOLikes : IDAO
    {
        private readonly Database db;
        private static DAOLikes instance = null!;

        private DAOLikes()
        {
            db = new Database(Config.ConnectionString.Value);
        }

        public static DAOLikes GetInstance()
        {
            return instance ??= new DAOLikes();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int idUtente, int idPost)
        {
            return db.Update(@$"
                DELETE FROM Likes
                WHERE idUtente = {idUtente}
                AND idPost = {idPost};
            ");
        }

        public Entity Find(int id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Entity e)
        {
            Like like = (Like)e;
            return db.Update(@$"
                INSERT INTO Posts (idUtente, idPost)
                VALUES ({like.IdUtente}, {like.IdPost});
            ");
        }

        public List<Entity> Read()
        {
            throw new NotImplementedException();
        }

        public List<int> ReadUserLikedPosts(int idUtente)
        {
            List<Dictionary<string, string>> righe = db.Read(@$"
                SELECT idPost FROM Likes WHERE idUtente = {idUtente};
            ");

            List<int> ris = new();
            foreach (Dictionary<string, string> riga in righe)
            {
                ris.Add(int.Parse(riga["idpost"]));
            }

            return ris;
        }

        public bool Update(Entity e)
        {
            throw new NotImplementedException();
        }
    }
}