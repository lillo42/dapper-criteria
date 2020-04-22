using System;

namespace Dapper.Criteria.Expressions.Ands
{
    public class AndComplexExpression : IExpression
    {
        public string Alias { get; set; }

        private readonly IExpression _left;
        private readonly IExpression _right;

        public AndComplexExpression(IExpression left, IExpression right)
        {
            _left = left ?? throw new ArgumentNullException(nameof(left));
            _right = right ?? throw new ArgumentNullException(nameof(right));
        }

        public string ToSql(ISqlDialect dialect)
        {
            return $"({_left.ToSql(dialect)} AND {_right.ToSql(dialect)})";
        }
    }
}