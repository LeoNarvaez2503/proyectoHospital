using System.Data.SqlClient;

namespace Login.Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            string dbName = builder.InitialCatalog;
            builder.InitialCatalog = "master";
            string serverConnStr = builder.ConnectionString;

            WaitForSqlServer(serverConnStr);

            using (var conn = new SqlConnection(serverConnStr))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"
                        IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{dbName}')
                        CREATE DATABASE [{dbName}]";
                    cmd.ExecuteNonQuery();
                }
            }

            string initScriptPath = Path.Combine(AppContext.BaseDirectory, "init.sql");
            if (!File.Exists(initScriptPath))
            {
                Console.WriteLine("[DatabaseInitializer] init.sql no encontrado, omitiendo inicializacion.");
                return;
            }

            string dbConnStr = connectionString;
            using (var conn = new SqlConnection(dbConnStr))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Usuario'";
                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        Console.WriteLine("[DatabaseInitializer] Tablas ya existen, omitiendo inicializacion.");
                        return;
                    }
                }
            }

            Console.WriteLine("[DatabaseInitializer] Ejecutando init.sql...");
            string script = File.ReadAllText(initScriptPath, System.Text.Encoding.UTF8);
            string[] batches = script.Split(new[] { "\r\nGO\r\n", "\nGO\n", "\rGO\r", "GO" }, StringSplitOptions.RemoveEmptyEntries);

            using (var conn = new SqlConnection(dbConnStr))
            {
                conn.Open();
                foreach (string batch in batches)
                {
                    string trimmed = batch.Trim();
                    if (string.IsNullOrEmpty(trimmed)) continue;
                    using (var cmd = new SqlCommand(trimmed, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            Console.WriteLine("[DatabaseInitializer] Base de datos inicializada correctamente.");
        }

        private static void WaitForSqlServer(string connectionString, int maxRetries = 30, int delaySeconds = 2)
        {
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    using var conn = new SqlConnection(connectionString);
                    conn.Open();
                    Console.WriteLine("[DatabaseInitializer] SQL Server listo.");
                    return;
                }
                catch (SqlException)
                {
                    Console.WriteLine($"[DatabaseInitializer] Esperando SQL Server... ({i + 1}/{maxRetries})");
                    Thread.Sleep(TimeSpan.FromSeconds(delaySeconds));
                }
            }
            throw new TimeoutException("SQL Server no estuvo disponible a tiempo.");
        }
    }
}
