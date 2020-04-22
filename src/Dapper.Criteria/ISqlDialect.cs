namespace Dapper.Criteria
{
    public interface ISqlDialect
    {
        bool LimitIsInTheEndOfQuery { get; }

        string Limit();

        string GetParameter(string parameter);
        
        string GetColumn(string column);
        
        string GetTable(string table);
        
        string GetSchema(string schema);

        string GetRawValue(object value);
    }
}