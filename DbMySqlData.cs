using System.Globalization;
using System.Diagnostics;
using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace DbMySql
{
    public class DbMySqlData
    {
        public static bool IsNull(object data) {
            return DBNull.Value.Equals(data);
        }

        public static bool IsNotNull(object data) {
            return !DbMySqlData.IsNull(data);
        }

        public static string GetTypeString (object value) {
            switch (DbMySqlData.GetValueTypeId(value)) {
                case 0:  return "Null";
                case 1:  return "Int";
                case 2:  return "Long";
                case 3:  return "Decimal";
                case 4:  return "String";
                case 5:  return "Char";
                case 6:  return "DateTime";
                case 7:  return "Boolean";
                case 8:  return "Byte";
                default: return "Undefined";
            }
        }

        public static int GetValueTypeId (object value) {
            if (value == null) return 0;

            switch (Type.GetTypeCode(value.GetType())) {
                case TypeCode.Int32:    return 1;
                case TypeCode.Int64:    return 2;
                case TypeCode.Decimal:  return 3;
                case TypeCode.String:   return 4;
                case TypeCode.Char:     return 5;
                case TypeCode.DateTime: return 6;
                case TypeCode.Boolean:  return 7;
                case TypeCode.Byte:     return 8;
                default:                return -1;
            }
        }

        public static void SetDbValueType (object value, MySqlParameter parameter) {
            switch (DbMySqlData.GetTypeString(value))
            {
                case "Int":      parameter.MySqlDbType = MySqlDbType.Int32; break;
                case "Long":     parameter.MySqlDbType = MySqlDbType.Int64; break;
                case "Decimal":  parameter.MySqlDbType = MySqlDbType.Decimal; break;
                case "String":   parameter.MySqlDbType = MySqlDbType.String; break;
                case "DateTime": parameter.MySqlDbType = MySqlDbType.DateTime; break;
                case "Boolean":  parameter.MySqlDbType = MySqlDbType.Bit; break;
                case "Byte":     parameter.MySqlDbType = MySqlDbType.Binary; break;
                case "Char":
                case "Null":
                default: break;
            }
        }
    }
}