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
            var record = db.ReadOne($"SELECT * FROM Utenti WHERE id = {id};");
            if (record is null) return null!;

            var utente = new Utente();
            utente.FromDictionary(record);

            return utente;
        }

        public Entity Find(string email)
        {
            var record = db.ReadOne($"SELECT * FROM Utenti WHERE email = '{email}';");
            if (record is null) return null!;

            var utente = new Utente();
            utente.FromDictionary(record);

            return utente;
        }

        public bool Insert(Entity e)
        {
            var utente = (Utente)e;
            int admin = utente.Amministratore ? 1 : 0;
            return db.Update(@$"
                INSERT INTO Utenti (
                    nominativo, amministratore, email, numero,
                    residenza, codiceFiscale, passwordHash
                ) VALUES (
                    '{utente.Nominativo}',
                    {admin},
                    '{utente.Email}',
                    '{utente.Numero}',
                    '{utente.Residenza}',
                    '{utente.CodiceFiscale.ToUpper()}',
                    HASHBYTES('SHA2_512', '{utente.PasswordHash}')
                );
            ");
        }

        public List<Entity> Read()
        {
            List<Entity> utenti = new();
            var tabella = db.Read("SELECT * FROM Utenti;");

            foreach (var riga in tabella)
            {
                Utente utente = new();
                utente.FromDictionary(riga);
                utenti.Add(utente);
            }

            return utenti;
        }

        public bool Update(Entity e)
        {
            Utente utente = (Utente)e;
            return db.Update(@$"
                UPDATE Utenti SET
                    nominativo = '{utente.Nominativo}',
                    email = '{utente.Email}',
                    numero = '{utente.Numero}',
                    residenza = '{utente.Residenza}',
                    codiceFiscale = '{utente.CodiceFiscale.ToUpper()}'
                WHERE id = {utente.Id};
            ");
        }

        public bool UpdatePassword(string email, string password)
        {
            return db.Update(@$"
                UPDATE Utenti SET
                    passwordHash = HASHBYTES('SHA2_512', '{password}')
                WHERE email = '{email}';
            ");
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