using MySql.Data.MySqlClient;
using System.Data;

namespace DbMySql
{
    public class DbMySqlParameters
    {
        private string projection;
        private object dbValue;

        public DbMySqlParameters (string projection, object dbValue) {
            this.projection = projection;
            this.dbValue = dbValue;
        }

        public MySqlParameter ReadParam () {
            MySqlParameter parameter = new MySqlParameter(parameterName: this.projection, value: null);
            DbMySqlData.SetDbValueType(this.dbValue, parameter);
            parameter.Value = this.dbValue;
            return parameter;
        }
    }
}