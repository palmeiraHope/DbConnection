using System;
using MySql.Data.MySqlClient;


namespace DbMySql
{
    public class DbMySqlConnection
    {
        private string ConnectionString = "server=localhost;user=root;database=escolinha;port=3306;password=root";
        private MySqlConnection Connection;

        public DbMySqlConnection () {
            this.Connection = new MySqlConnection(this.ConnectionString);
        }

        public DbMySqlConnection (string ConnectionString) : this () {
            this.ConnectionString = ConnectionString;
        }

        public void Open () {
            if (this.Connection.State == System.Data.ConnectionState.Closed) return;
            this.Connection.Open();
        }

        public void Close () {
            if (this.Connection.State != System.Data.ConnectionState.Open) return;
            this.Connection.Close();
        }

        // SELECT * FROM `ALUNO`;
        // SELECT * FROM `ALUNO` WHERE (`ID` = :ID);
        // 
        //                                     [:ID, 2]
        // SELECT * FROM `ALUNO` WHERE (`ID` = 2);
        private MySqlCommand PrepareCommand (string query, params DbMySqlParameters[] parameters) {
            MySqlCommand command = new MySqlCommand(query, this.Connection);

            foreach (DbMySqlParameters parameter in parameters) {
                if (parameter == null) continue;
                command.Parameters.Add(parameter.ReadParam());
            }

            return command;
        }

        // utilização esperada SELECT
        public MySqlDataReader ExecuteReader (string query, params DbMySqlParameters[] parameters) {
            return this.PrepareCommand(query, parameters).ExecuteReader();
        }

        // utilização esperada UPDATE e DELETE
        public void ExecuteNonQuery (string query, params DbMySqlParameters[] parameters) {
            this.PrepareCommand(query, parameters).ExecuteNonQuery();
        }

        // utilização esperada INSERT
        public long ExecuteScalar (string query, params DbMySqlParameters[] parameters) {
            object scalar = this.PrepareCommand(query, parameters).ExecuteScalar();

            if (DbMySqlData.IsNull(scalar))
                return -1; // erro não foi inserido o valor
            else
                return Convert.ToInt64(scalar);
        }
    }
}
