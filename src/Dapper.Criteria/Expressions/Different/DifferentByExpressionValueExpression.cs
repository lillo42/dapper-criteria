using System;
using System.Text;

namespace Dapper.Criteria.Expressions.Different
{
    public class DifferentByExpressionValueExpression : IExpression
    {
        private readonly string _column;
        private readonly IExpression _expression;

        public DifferentByExpressionValueExpression(string column, IExpression value, string @alias)
        {
            _column = column ?? throw new ArgumentNullException(nameof(column));
            _expression = value ?? throw new ArgumentNullException(nameof(value));
            Alias = alias;
        }

        public string Alias { get; set; }

        public void SetExpression(ISqlDialect dialect, StringBuilder query)
        {
            query.Append(dialect.GetAlias(Alias))
                .Append(dialect.GetColumn(_column))
                .Append(" <> (");

            _expression.SetExpression(dialect, query);
            
            query.Append(")");
        }
    }
}