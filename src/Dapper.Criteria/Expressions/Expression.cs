using Dapper.Criteria.Expressions.Eqs;
using Dapper.Criteria.Expressions.Likes;
using Dapper.Criteria.Expressions.Null;

namespace Dapper.Criteria.Expressions
{
    public static class Expression
    {
        #region Like
        public static IExpression Like(string column, string parameter)
            => new LikeByParameterExpression(column, parameter, null);
        
        public static IExpression Like(string column, string parameter, string alias)
            => new LikeByParameterExpression(column, parameter, alias);
        
        public static IExpression LikeLiteralValue(string column, string value)
            => new LikeByValueExpression(column, value, null);
        
        public static IExpression LikeLiteralValue(string column, string value, string alias)
            => new LikeByValueExpression(column, value, alias);
        #endregion

        #region Eq
        public static IExpression Eq(string column, string parameter)
            => new EqualByParameterExpression(column, parameter, null);
        
        public static IExpression Eq(string column, string parameter, string alias)
            => new EqualByParameterExpression(column, parameter, alias);


        public static IExpression EqLiteralValue(string column, object value)
            => EqLiteralValue(column, value, null);
        
        public static IExpression EqLiteralValue(string column, object value, string alias)
        {
            if (value == null)
            {
                return new IsNullExpression(column, alias);
            }
            
            return new EqualByValueExpression(column, value, alias);
        } 
        #endregion
        
    }
}