using Dapper.Criteria.Expressions.Eqs;
using Dapper.Criteria.Expressions.Greater;
using Dapper.Criteria.Expressions.Less;
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

        #region Greater
        public static IExpression Greater(string column, string parameter)
            => new GreaterByParameterExpression(column, parameter, null);
        
        public static IExpression Greater(string column, string parameter, string alias)
            => new GreaterByParameterExpression(column, parameter, alias);
        
        public static IExpression Greater(string column, IExpression parameter)
            => new GreaterByExpressionValueExpression(column, parameter, null);
        
        public static IExpression Greater(string column, IExpression parameter, string alias)
            => new GreaterByExpressionValueExpression(column, parameter, alias);
        
        public static IExpression GreaterLiteralValue(string column, object value)
            => new GreaterByLiteralValueExpression(column, value, null);
        
        public static IExpression GreaterLiteralValue(string column, object value, string alias)
            => new GreaterByLiteralValueExpression(column, value, alias);
        #endregion

        #region Greater or Equal
        public static IExpression GreaterOrEq(string column, string parameter)
            => new GreaterOrEqualByParameterExpression(column, parameter, null);
        
        public static IExpression GreaterOrEq(string column, string parameter, string alias)
            => new GreaterOrEqualByParameterExpression(column, parameter, alias);
        #endregion
        
        #region Less
        public static IExpression Less(string column, string parameter)
            => new LessExpression(column, parameter, null);
        
        public static IExpression Less(string column, string parameter, string alias)
            => new LessExpression(column, parameter, alias);
        #endregion
        
        #region Less or Equal
        public static IExpression LessOrEq(string column, string parameter)
            => new LessOrEqualExpression(column, parameter, null);
        
        public static IExpression LessOrEq(string column, string parameter, string alias)
            => new LessOrEqualExpression(column, parameter, alias);
        #endregion
    }
}