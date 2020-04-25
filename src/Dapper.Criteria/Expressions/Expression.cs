using Dapper.Criteria.Expressions.Eqs;
using Dapper.Criteria.Expressions.Greater;
using Dapper.Criteria.Expressions.GreaterOrEqual;
using Dapper.Criteria.Expressions.In;
using Dapper.Criteria.Expressions.Less;
using Dapper.Criteria.Expressions.LessOrEqual;
using Dapper.Criteria.Expressions.Likes;
using Dapper.Criteria.Expressions.NotEqs;
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
        
        
        public static IExpression Eq(string column, IExpression expression)
            => new EqualByExpressionValueExpression(column, expression, null);
        public static IExpression Eq(string column, IExpression expression, string alias)
            => new EqualByExpressionValueExpression(column, expression, alias);
        
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
        
        #region Not Eq
        public static IExpression NotEq(string column, string parameter)
            => new NotEqualByParameterExpression(column, parameter, null);
        
        public static IExpression NotEq(string column, string parameter, string alias)
            => new NotEqualByParameterExpression(column, parameter, alias);

        public static IExpression NotEq(string column, IExpression expression)
            => new NotEqualByExpressionValueExpression(column, expression, null);
        public static IExpression NotEq(string column, IExpression expression, string alias)
            => new NotEqualByExpressionValueExpression(column, expression, alias);
        
        public static IExpression NotEqLiteralValue(string column, object value)
            => NotEqLiteralValue(column, value, null);
        
        public static IExpression NotEqLiteralValue(string column, object value, string alias)
        {
            if (value == null)
            {
                return new IsNotNullExpression(column, alias);
            }
            
            return new NotEqualByValueExpression(column, value, alias);
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
        
        public static IExpression GreaterOrEq(string column, IExpression parameter)
            => new GreaterByExpressionValueExpression(column, parameter, null);
        
        public static IExpression GreaterOrEq(string column, IExpression parameter, string alias)
            => new GreaterOrEqualByExpressionValueExpression(column, parameter, alias);
        
        public static IExpression GreaterOrEqLiteralValue(string column, object value)
            => new GreaterOrEqualByLiteralValueExpression(column, value, null);
        
        public static IExpression GreaterOrEqLiteralValue(string column, object value, string alias)
            => new GreaterOrEqualByLiteralValueExpression(column, value, alias);
        #endregion
        
        #region Less
        public static IExpression Less(string column, string parameter)
            => new LessByParameterExpression(column, parameter, null);
        
        public static IExpression Less(string column, string parameter, string alias)
            => new LessByParameterExpression(column, parameter, alias);
        
        public static IExpression Less(string column, IExpression expression)
            => new LessByExpressionValueExpression(column, expression, null);
        public static IExpression Less(string column, IExpression expression, string alias)
            => new LessByExpressionValueExpression(column, expression, alias);
        
        public static IExpression LessLiteralValue(string column, object value)
            => new LessByLiteralValueExpression(column, value, null);
        
        public static IExpression LessLiteralValue(string column, object value, string alias)
            => new LessByLiteralValueExpression(column, value, alias);
        #endregion
        
        #region Less or Equal
        public static IExpression LessOrEq(string column, string parameter)
            => new LessOrEqualByParameterExpression(column, parameter, null);
        
        public static IExpression LessOrEq(string column, string parameter, string alias)
            => new LessOrEqualByParameterExpression(column, parameter, alias);
        
        public static IExpression LessOrEq(string column, IExpression expression)
            => new LessOrEqualByExpressionValueExpression(column, expression, null);
        public static IExpression LessOrEq(string column, IExpression expression, string alias)
            => new LessOrEqualByExpressionValueExpression(column, expression, alias);
        
        public static IExpression LessOrEqLiteralValue(string column, object value)
            => new LessOrEqualByLiteralValueExpression(column, value, null);
        public static IExpression LessOrEqLiteralValue(string column, object value, string alias)
            => new LessOrEqualByLiteralValueExpression(column, value, alias);
        #endregion

        #region Null
        public static IExpression IsNull(string column)
            => new IsNullExpression(column, null);

        public static IExpression IsNull(string column, string alias)
            => new IsNullExpression(column, alias);
        #endregion
        
        #region Not Null
        public static IExpression IsNotNull(string column)
            => new IsNotNullExpression(column, null);

        public static IExpression IsNotNull(string column, string alias)
            => new IsNotNullExpression(column, alias);
        #endregion
        
        
        #region In
        public static IExpression In(string column, params string[] parameters)
            => new InByParametersExpression(column, parameters, null);
        
        public static IExpression InWithAlias(string column, string alias, params string[] parameter)
            => new InByParametersExpression(column, parameter, alias);
        
        public static IExpression In(string column, IExpression parameter)
            => new InByExpressionValueExpression(column, parameter, null);
        
        public static IExpression In(string column, IExpression parameter, string alias)
            => new InByExpressionValueExpression(column, parameter, alias);
        
        public static IExpression InLiteralValue(string column, params object[] value)
            => new InByLiteralValueExpression(column, value, null);
        
        public static IExpression InLiteralValueWithAlias(string column,  string alias, params object[] value)
            => new InByLiteralValueExpression(column, value, alias);
        #endregion
        
    }
}