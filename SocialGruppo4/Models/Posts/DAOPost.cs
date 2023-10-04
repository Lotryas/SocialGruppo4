using SocialGruppo4.Models.Utenti;
using Utility;

namespace SocialGruppo4.Models.Post
{
    public class DAOPost : IDAO
    {
        private readonly Database db;
        private static DAOPost instance = null;

        private DAOPost()
        {
            db = new Database("ProjectWork");
        }

        public static DAOPost getInstance()
        {
            if (instance == null)
            {
                instance = new DAOPost();
            }

            return instance;
        }

        public bool Delete(int id)
        {
            return db.Update($"delete from Posts where id = {id}");
        }

        public Entity Find(int id)
        {
            foreach(Entity e in Read())
            {
                if(e.Id == id)
                {
                    return e;
                };
            }

            return null;
        }

        public bool Insert(Entity e)
        {
            return db.Update(
                            $"insert into Posts " +
                            $"(idUtente, idPadre, contenuto, dataEora, miPiace) " +
                            $"values" +
                            $"('{((Post)e).IdUtente}','{((Post)e).IdPadre}','{((Post)e).Contenuto}','{((Post)e).DataEora}','{((Post)e).MiPiace}')"
                            );
        }

        public List<Entity> Read()
        {
            List<Entity> ris = new List<Entity>();

            List<Dictionary<string, string>> righe = db.Read("select * from Posts");

            foreach(Dictionary<string, string> riga in righe)
            {
                Post p = new Post();

                p.FromDictionary(riga);

                ris.Add(p);
            }

            return ris;
        }

        public bool Update(Entity e)
        {
            return db.Update(
                            $"update Posts set " +
                            $"idUtente = '{((Post)e).IdUtente}', " +
                            $"idPadre = '{((Post)e).IdPadre}', " +
                            $"contenuto = {((Post)e).Contenuto}', " +
                            $"dataEora = {((Post)e).DataEora}', " +
                            $"miPiace = {((Post)e).MiPiace}"
                            );
        }
    }
}