using MySql.Data.MySqlClient;

namespace contatos_infrastructure.persistence.mysql;

public static class ConexaoMySql
{
    public static MySqlConnection CriaConexaoMySql(string connectionString)
    {
        MySqlConnection mySqlConnection = new MySqlConnection(connectionString);

        mySqlConnection.Open();

        using (var cmd = mySqlConnection.CreateCommand())
        {
            cmd.CommandText = @"
                -- Cria o banco de dados se ele não existir
                CREATE DATABASE IF NOT EXISTS contatos;

                -- Usa o banco de dados criado ou existente
                USE contatos;

                -- Cria uma tabela se ela não existir
                CREATE TABLE IF NOT EXISTS contato (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    nome VARCHAR(255) NOT NULL,
                    telefone VARCHAR(15) NOT NULL,
                    email VARCHAR(255) NOT NULL
                );";

            cmd.ExecuteNonQuery();
        }

        return mySqlConnection;
    }
}
