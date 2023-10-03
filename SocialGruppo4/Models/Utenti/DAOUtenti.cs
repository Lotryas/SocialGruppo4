using Utility;

namespace SocialGruppo4.Models.Utenti
{
    public class DAOUtenti : IDAO
    {
        private readonly Database db;
        private static DAOUtenti? instance;

        private DAOUtenti()
        {
            db = new Database(Config.ConnectionString.Value);
        }

        public static DAOUtenti GetInstance()
        {
            return instance ??= new DAOUtenti();
        }

        public bool Delete(int id)
        {
            return db.Update($"DELETE FROM Utenti WHERE id = {id}");
        }

        public Entity Find(int id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Entity e)
        {
            throw new NotImplementedException();
        }

        public List<Entity> Read()
        {
            throw new NotImplementedException();
        }

        public bool Update(Entity e)
        {
            throw new NotImplementedException();
        }

        public Entity? Find(string email, string plainPassword)
        {
            var record = db.ReadOne(@$"
                SELECT * FROM Utenti
                WHERE email = '{email}'
                AND passwordHash = HASHBYTES('SHA2_512', '{plainPassword}');
            ");

            if (record is null) return null;

            var utente = new Utente();
            utente.FromDictionary(record);

            return utente;
        }
    }
}