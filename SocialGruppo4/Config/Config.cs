namespace SocialGruppo4.Config
{
    public static class ConnectionString
    {
        public static string Value { get; private set; } = "";

        /*
            Imposta il valore della stringa di connessione.
            Valori possibili:
                Windows: Data Source=DESKTOP-9GDQRUC;Initial Catalog=NomeDatabase;Integrated Security=True;
                Linux: Server=localhost;Database=NomeDatabase;User Id=sa;Password=SQLServerDevPassword!;
         */
        public static void SetConnectionString(string connectionString) => Value = connectionString;
    }
}