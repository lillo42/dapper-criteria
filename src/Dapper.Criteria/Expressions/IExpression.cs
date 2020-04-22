namespace Dapper.Criteria.Expressions
{
    public interface IExpression
    {
        string ToSql(ISqlDialect dialect);
    }
}