using System.Text;

namespace Dapper.Criteria.SqlDialects
{
    public class PostgresDialect : ISqlDialect
    {
        public void SetLimit(int limit, StringBuilder query) 
            => query.Append("LIMIT ").Append(limit);

        public string GetParameter(string parameter)
        {
            if (parameter.StartsWith(":"))
            {
                return parameter;
            }

            if (parameter.StartsWith("@"))
            {
                return ":" + parameter.Remove(0, 1);
            }

            return ":" + parameter;
            
        }

        public string GetColumn(string column)
            => Quote(column);

        public string GetTable(string table) 
            => Quote(table);

        public string GetSchema(string schema)
        {
            if (string.IsNullOrEmpty(schema))
            {
                return "public";
            }

            return Quote(schema);
        }

        public string GetRawValue(object value)
        {
            switch (value)
            {
                case bool _:
                    return value.ToString().ToLower();
                case byte _:
                case sbyte _:
                case short _:
                case ushort _:
                case int _:
                case uint _:
                case long _:
                case ulong _:
                case float _:
                case double _:
                case decimal _:
                    return value.ToString();
                default:
                    return "'" + value + "'";
            }
        }

        private static string Quote(string value)
            => "\"" + value + "\"";
    }
}