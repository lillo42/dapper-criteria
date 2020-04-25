using System;
using System.Text;

namespace Dapper.Criteria.Expressions.Ands
{
    public class AndExpression : IExpression
    {
        public string Alias { get; set; }

        private readonly IExpression _expression;

        public AndExpression(IExpression expression)
        {
            _expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public void SetExpression(ISqlDialect dialect, StringBuilder query)
        {
            query.Append("AND ");
            _expression.SetExpression(dialect, query);
        }
    }
}