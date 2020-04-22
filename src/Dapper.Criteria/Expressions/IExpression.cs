namespace Dapper.Criteria.Expressions
{
    public interface IExpression
    {
        string Alias { get; set; }
        string ToSql(ISqlDialect dialect);
    }
}