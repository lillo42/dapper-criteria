namespace Dapper.Criteria
{
    public interface ISqlDialect
    {
        string GetColumn(string column);
        
        string GetTable(string table);
        
        string GetSchema(string schema);
    }
}