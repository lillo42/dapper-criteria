using System;

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

        public string ToSql(ISqlDialect dialect)
        {
            return $"AND {_expression.ToSql(dialect)}";
        }
    }
}