using System;
using System.Text;

namespace Dapper.Criteria.Expressions.Or
{
    public class OrExpression : IExpression
    {
        public string Alias { get; set; }

        private readonly IExpression _expression;

        public OrExpression(IExpression expression)
        {
            _expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public void SetExpression(ISqlDialect dialect, StringBuilder query)
        {
            query.Append("OR ");
            _expression.SetExpression(dialect, query);
        }
    }
}