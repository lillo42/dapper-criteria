using System;

namespace Dapper.Criteria.Expressions.Ors
{
    public class OrComplexExpression : IExpression
    {
        public string Alias { get; set; }

        private readonly IExpression _left;
        private readonly IExpression _right;

        public OrComplexExpression(IExpression left, IExpression right)
        {
            _left = left ?? throw new ArgumentNullException(nameof(left));
            _right = right ?? throw new ArgumentNullException(nameof(right));
        }

        public string ToSql(ISqlDialect dialect) 
            => $"({_left.ToSql(dialect)} OR {_right.ToSql(dialect)})";
    }
}