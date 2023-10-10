using SocialGruppo4.Models.Utenti;
using Utility;

namespace SocialGruppo4.Models.Post
{
    public class DAOPost : IDAO
    {
        private readonly Database db;
        private static DAOPost instance = null!;

        private DAOPost()
        {
            db = new Database(Config.ConnectionString.Value);
        }

        public static DAOPost GetInstance()
        {
            return instance ??= new DAOPost();
        }

        public bool Delete(int id)
        {
            return db.Update($"delete from Posts where id = {id};");
        }

        public Entity Find(int id)
        {
            foreach (Entity e in Read())
            {
                if (e.Id == id)
                {
                    return e;
                };
            }

            return null!;
        }

        public bool Insert(Entity e)
        {
            Post p = (Post)e;
            return db.Update(@$"
                INSERT INTO Posts (idUtente, idPadre, contenuto, dataEora, miPiace, immagine)
                VALUES (
                    {p.IdUtente},
                    {p.IdPadre},
                    '{p.Contenuto}',
                    '{p.DataEora}',
                    {p.MiPiace},
                    '{p.Immagine}'
                );
            ");
        }

        public List<Entity> Read()
        {
            List<Entity> ris = new();

            List<Dictionary<string, string>> righe = db.Read("select * from Posts;");

            foreach (Dictionary<string, string> riga in righe)
            {
                Post p = new();
                p.FromDictionary(riga);
                ris.Add(p);
            }

            return ris;
        }

        public bool Update(Entity e)
        {
            Post p = (Post)e;
            return db.Update(@$"
                UPDATE Posts SET
                    idUtente = {p.IdUtente},
                    idPadre = {p.IdPadre},
                    contenuto = '{p.Contenuto}',
                    dataEora = '{p.DataEora}',
                    miPiace = {p.MiPiace},
                    immagine = '{p.Immagine}'
                WHERE id = {p.Id};
            ");
        }
    }
}