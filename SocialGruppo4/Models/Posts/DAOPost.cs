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
            string idPadre = p.IdPadre?.ToString() ?? "NULL";
            return db.Update(@$"
                INSERT INTO Posts (idUtente, idPadre, contenuto, dataEora, miPiace, immagine)
                VALUES (
                    {p.IdUtente},
                    {idPadre},
                    '{Entity.PulisciApici(p.Contenuto)}',
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
                    contenuto = '{Entity.PulisciApici(p.Contenuto)}',
                    dataEora = '{p.DataEora}',
                    miPiace = {p.MiPiace},
                    immagine = '{p.Immagine}'
                WHERE id = {p.Id};
            ");
        }

        public List<Entity> Latest()
        {
            List<Entity> ris = new();

            List<Dictionary<string, string>> righe = db.Read(@"
                SELECT * FROM Posts ORDER BY id DESC;
            ");

            foreach (Dictionary<string, string> riga in righe)
            {
                Post p = new();
                p.FromDictionary(riga);
                p.Utente = (Utente?)DAOUtenti.GetInstance().Find(p.IdUtente);
                ris.Add(p);
            }

            return ris;
        }

        public List<Entity> LatestFollowing(int idUtente)
        {
            List<Entity> ris = new();

            List<Dictionary<string, string>> righe = db.Read(@$"
                SELECT * FROM Posts
                WHERE Posts.idUtente IN (
                    SELECT Followers.idFollower
                    FROM Followers
                    WHERE Followers.idUtente = {idUtente}
                )
                OR Posts.idUtente = {idUtente}
                ORDER BY id DESC;
            ");

            foreach (Dictionary<string, string> riga in righe)
            {
                Post p = new();
                p.FromDictionary(riga);
                p.Utente = (Utente)DAOUtenti.GetInstance().Find(p.IdUtente);
                ris.Add(p);
            }

            return ris;
        }

        public List<List<Entity>> FullPost(int id)
        {
            List<Entity> postOriginale = new() { Find(id) };

            //{ postOriginale, threadCommenti<commento-risposte>, threadCommenti<commento-risposte>....}
            List<List<Entity>> ris = new() { postOriginale };

            List<Entity> threadCommenti = new();


            List<int> idRispostaPost = new();

            foreach (Entity e in Read())
                if (e is Post && ((Post)e).IdPadre == id)
                    idRispostaPost.Add(((Post)e).Id);

            foreach (int idr in idRispostaPost)
            {
                threadCommenti.Add(Find(idr));

                List<int?> idRisposte = new();

                foreach (Entity e in Read())
                    if (e is Post && ((Post)e).IdPadre != null)
                        if (((Post)e).IdPadre == idr || idRisposte.Contains(((Post)e).IdPadre) && !idRisposte.Contains(((Post)e).Id))
                            idRisposte.Add(((Post)e).Id);

                foreach (int idRis in idRisposte)
                    threadCommenti.Add(Find(idRis));

                ris.Add(threadCommenti);

                threadCommenti.Clear();
                idRisposte.Clear();
            }


            return ris;
        }
    }
}