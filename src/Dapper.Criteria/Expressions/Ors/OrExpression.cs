using System;

namespace Dapper.Criteria.Expressions.Ors
{
    public class OrExpression : IExpression
    {
        public string Alias { get; set; }

        private readonly IExpression _expression;

        public OrExpression(IExpression expression)
        {
            _expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public string ToSql(ISqlDialect dialect) 
            => $"OR {_expression.ToSql(dialect)}";
    }
}