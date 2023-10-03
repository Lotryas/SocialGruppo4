using System.Data.SqlClient;

namespace Utility
{
    public class Database
    {
        public SqlConnection Connection { get; set; }

        public Database(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
        }

        public Database(string nomeDB, string server = "LAPTOP-B71E9AKC")
        {
            Connection = new SqlConnection(
                $"Data Source = {server}; " +
                $"Initial Catalog = {nomeDB}; " +
                $"Integrated Security = True;"
            );
        }

        public List<Dictionary<string, string>> Read(string query)
        {
            List<Dictionary<string, string>> ris = new List<Dictionary<string, string>>();

            Connection.Open();

            SqlCommand cmd = new SqlCommand(query, Connection);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Dictionary<string, string> riga = new Dictionary<string, string>();

                for (int i = 0; i < dr.FieldCount; i++)
                {
                    riga.Add(dr.GetName(i).ToLower(), dr.GetValue(i).ToString());
                }

                ris.Add(riga);
            }

            dr.Close();
            Connection.Close();

            return ris;
        }

        public bool Update(string query)
        {
            try
            {
                Connection.Open();

                SqlCommand cmd = new SqlCommand(query, Connection);

                int affette = cmd.ExecuteNonQuery();

                return affette > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"QUERY: \n{query}");

                return false;
            }
            finally
            {
                Connection.Close();
            }
        }

        public Dictionary<string, string> ReadOne(string query)
        {
            try
            {
                return Read(query)[0];
            }
            catch
            {
                return null;
            }
        }
    }
}
