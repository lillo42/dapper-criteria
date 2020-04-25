using System.Text;

namespace Dapper.Criteria
{
    public interface ISqlDialect
    {
        void SetLimit(int limit, StringBuilder query);

        string GetParameter(string parameter);
        
        string GetColumn(string column);
        
        string GetTable(string table);
        
        string GetSchema(string schema);

        string GetRawValue(object value);
    }
}