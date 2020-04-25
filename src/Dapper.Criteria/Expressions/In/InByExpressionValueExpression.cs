using System;
using System.Text;

namespace Dapper.Criteria.Expressions.In
{
    public class InByExpressionValueExpression : IExpression
    {
        private readonly string _column;
        private readonly IExpression _expression;

        public InByExpressionValueExpression(string column, IExpression expression, string @alias)
        {
            _column = column ?? throw new ArgumentNullException(nameof(column));
            _expression = expression ?? throw new ArgumentNullException(nameof(expression));
            Alias = alias;
        }

        public string Alias { get; set; }

        public void SetExpression(ISqlDialect dialect, StringBuilder query)
        {
            query
                .Append(dialect.GetAlias(Alias))
                .Append(dialect.GetColumn(_column))
                .Append(" IN (");
            
            _expression.SetExpression(dialect, query);

           query.Append(")");
        }
    }
}