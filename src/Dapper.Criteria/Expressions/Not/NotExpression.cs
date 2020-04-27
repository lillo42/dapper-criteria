using System;
using System.Text;

namespace Dapper.Criteria.Expressions.Not
{
    public class NotExpression : IExpression
    {
        private readonly IExpression _expression;

        public NotExpression(IExpression expression)
        {
            _expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public string Alias { get; set; }

        public void SetExpression(ISqlDialect dialect, StringBuilder query)
        {
            query.Append("NOT ");
            _expression.SetExpression(dialect, query);
        }
    }
}