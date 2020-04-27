using Dapper.Criteria.Expressions.Different;
using Dapper.Criteria.Expressions.Eqs;
using Dapper.Criteria.Expressions.Greater;
using Dapper.Criteria.Expressions.GreaterOrEqual;
using Dapper.Criteria.Expressions.In;
using Dapper.Criteria.Expressions.Less;
using Dapper.Criteria.Expressions.LessOrEqual;
using Dapper.Criteria.Expressions.Likes;
using Dapper.Criteria.Expressions.Null;

namespace Dapper.Criteria.Expressions
{
    public class Expression
    {
        private readonly bool _isNot;

        private Expression(bool isNot)
        {
            _isNot = isNot;
        }


        public static Expression Instance { get; } = new Expression(false);

        public Expression Not { get; } = new Expression(true);

        #region Like
        public IExpression Like(string column, string parameter)
            => new LikeByParameterExpression(column, parameter, null);
        
        public IExpression Like(string column, string parameter, string alias)
            => new LikeByParameterExpression(column, parameter, alias);
        
        public IExpression LikeLiteralValue(string column, string value)
            => new LikeByValueExpression(column, value, null);
        
        public IExpression LikeLiteralValue(string column, string value, string alias)
            => new LikeByValueExpression(column, value, alias);
        #endregion

        #region Eq
        public IExpression Eq(string column, string parameter)
            => new EqualByParameterExpression(column, parameter, null);
        
        public IExpression Eq(string column, string parameter, string alias)
            => new EqualByParameterExpression(column, parameter, alias);

        public IExpression Eq(string column, IExpression expression)
            => new EqualByExpressionValueExpression(column, expression, null);
        public IExpression Eq(string column, IExpression expression, string alias)
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
        
        #region Different
        public IExpression NotEq(string column, string parameter)
            => new DifferentEqualByParameterExpression(column, parameter, null);
        
        public IExpression NotEq(string column, string parameter, string alias)
            => new DifferentEqualByParameterExpression(column, parameter, alias);

        public IExpression NotEq(string column, IExpression expression)
            => new DifferentByExpressionValueExpression(column, expression, null);
        public IExpression NotEq(string column, IExpression expression, string alias)
            => new DifferentByExpressionValueExpression(column, expression, alias);
        
        public IExpression NotEqLiteralValue(string column, object value)
            => NotEqLiteralValue(column, value, null);
        
        public IExpression NotEqLiteralValue(string column, object value, string alias)
        {
            if (value == null)
            {
                return new IsNotNullExpression(column, alias);
            }
            
            return new DifferentEqualByValueExpression(column, value, alias);
        } 
        #endregion

        #region Greater
        public IExpression Greater(string column, string parameter)
            => new GreaterByParameterExpression(column, parameter, null);
        
        public IExpression Greater(string column, string parameter, string alias)
            => new GreaterByParameterExpression(column, parameter, alias);
        
        public IExpression Greater(string column, IExpression parameter)
            => new GreaterByExpressionValueExpression(column, parameter, null);
        
        public IExpression Greater(string column, IExpression parameter, string alias)
            => new GreaterByExpressionValueExpression(column, parameter, alias);
        
        public IExpression GreaterLiteralValue(string column, object value)
            => new GreaterByLiteralValueExpression(column, value, null);
        
        public IExpression GreaterLiteralValue(string column, object value, string alias)
            => new GreaterByLiteralValueExpression(column, value, alias);
        #endregion

        #region Greater or Equal
        public IExpression GreaterOrEq(string column, string parameter)
            => new GreaterOrEqualByParameterExpression(column, parameter, null);
        
        public IExpression GreaterOrEq(string column, string parameter, string alias)
            => new GreaterOrEqualByParameterExpression(column, parameter, alias);
        
        public IExpression GreaterOrEq(string column, IExpression parameter)
            => new GreaterByExpressionValueExpression(column, parameter, null);
        
        public IExpression GreaterOrEq(string column, IExpression parameter, string alias)
            => new GreaterOrEqualByExpressionValueExpression(column, parameter, alias);
        
        public IExpression GreaterOrEqLiteralValue(string column, object value)
            => new GreaterOrEqualByLiteralValueExpression(column, value, null);
        
        public IExpression GreaterOrEqLiteralValue(string column, object value, string alias)
            => new GreaterOrEqualByLiteralValueExpression(column, value, alias);
        #endregion
        
        #region Less
        public IExpression Less(string column, string parameter)
            => new LessByParameterExpression(column, parameter, null);
        
        public IExpression Less(string column, string parameter, string alias)
            => new LessByParameterExpression(column, parameter, alias);
        
        public IExpression Less(string column, IExpression expression)
            => new LessByExpressionValueExpression(column, expression, null);
        public IExpression Less(string column, IExpression expression, string alias)
            => new LessByExpressionValueExpression(column, expression, alias);
        
        public IExpression LessLiteralValue(string column, object value)
            => new LessByLiteralValueExpression(column, value, null);
        
        public IExpression LessLiteralValue(string column, object value, string alias)
            => new LessByLiteralValueExpression(column, value, alias);
        #endregion
        
        #region Less or Equal
        public IExpression LessOrEq(string column, string parameter)
            => new LessOrEqualByParameterExpression(column, parameter, null);
        
        public IExpression LessOrEq(string column, string parameter, string alias)
            => new LessOrEqualByParameterExpression(column, parameter, alias);
        
        public IExpression LessOrEq(string column, IExpression expression)
            => new LessOrEqualByExpressionValueExpression(column, expression, null);
        public IExpression LessOrEq(string column, IExpression expression, string alias)
            => new LessOrEqualByExpressionValueExpression(column, expression, alias);
        
        public IExpression LessOrEqLiteralValue(string column, object value)
            => new LessOrEqualByLiteralValueExpression(column, value, null);
        public IExpression LessOrEqLiteralValue(string column, object value, string alias)
            => new LessOrEqualByLiteralValueExpression(column, value, alias);
        #endregion

        #region Null
        public IExpression IsNull(string column)
            => new IsNullExpression(column, null);

        public IExpression IsNull(string column, string alias)
            => new IsNullExpression(column, alias);
        #endregion
        
        #region Not Null
        public IExpression IsNotNull(string column)
            => new IsNotNullExpression(column, null);

        public IExpression IsNotNull(string column, string alias)
            => new IsNotNullExpression(column, alias);
        #endregion
        
        
        #region In
        public IExpression In(string column, params string[] parameters)
            => new InByParametersExpression(column, parameters, null);
        
        public IExpression InWithAlias(string column, string alias, params string[] parameter)
            => new InByParametersExpression(column, parameter, alias);
        
        public IExpression In(string column, IExpression parameter)
            => new InByExpressionValueExpression(column, parameter, null);
        
        public IExpression In(string column, IExpression parameter, string alias)
            => new InByExpressionValueExpression(column, parameter, alias);
        
        public IExpression InLiteralValue(string column, params object[] value)
            => new InByLiteralValueExpression(column, value, null);
        
        public IExpression InLiteralValueWithAlias(string column,  string alias, params object[] value)
            => new InByLiteralValueExpression(column, value, alias);
        #endregion
        
    }
}