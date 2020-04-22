namespace Dapper.Criteria.Expressions
{
    public static class Expression
    {
        public static IExpression Like(string column, string value)
            => new LikeExpression(column, value, null);
        
        public static IExpression Like(string column, string value, string alias)
            => new LikeExpression(column, value, alias);
    }
}