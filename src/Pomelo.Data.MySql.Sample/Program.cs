namespace Pomelo.Data.MySql.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var conn = GetMySqlConnection())
            {
                conn.CancelQuery(1);
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from resume";
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandTimeout = 1;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            System.Console.WriteLine(reader.GetString(0));
                        }
                    }
                }
            }
        }

        static MySqlConnection GetMySqlConnection(bool open = true,
            bool convertZeroDatetime = false, bool allowZeroDatetime = false)
        {
            var csb = new MySqlConnectionStringBuilder("server=localhost;userid=root;pwd=1234abcd;port=3306;database=bzwdev;sslmode=none;")
            {
                AllowZeroDateTime = allowZeroDatetime,
                ConvertZeroDateTime = convertZeroDatetime
            };
            var conn = new MySqlConnection(csb.ConnectionString);
            if (open) conn.Open();
            return conn;
        }
    }
}
