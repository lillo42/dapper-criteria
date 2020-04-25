using System;
using System.Text;

namespace Dapper.Criteria.Expressions.LessOrEqual
{
    public class LessOrEqualByExpressionValueExpression : IExpression
    {
        public string Alias { get; set; }

        private readonly string _column;
        private readonly IExpression _expression;

        public LessOrEqualByExpressionValueExpression(string column, IExpression expression, string @alias)
        {
            _column = column ?? throw new ArgumentNullException(nameof(column));
            _expression = expression ?? throw new ArgumentNullException(nameof(expression));
            Alias = @alias;
        }

        public void SetExpression(ISqlDialect dialect, StringBuilder query)
        {
            query
                .Append(dialect.GetAlias(Alias))
                .Append(dialect.GetColumn(_column))
                .Append(" <= (");

            _expression.SetExpression(dialect, query);
            
            query.Append(")");
        }
    }
}