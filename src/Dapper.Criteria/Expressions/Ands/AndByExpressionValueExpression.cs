using System;
using System.Text;

namespace Dapper.Criteria.Expressions.Ands
{
    public class AndByExpressionValueExpression : IExpression
    {
        public string Alias { get; set; }

        private readonly IExpression _left;
        private readonly IExpression _right;

        public AndByExpressionValueExpression(IExpression left, IExpression right)
        {
            _left = left ?? throw new ArgumentNullException(nameof(left));
            _right = right ?? throw new ArgumentNullException(nameof(right));
        }

        public void SetExpression(ISqlDialect dialect, StringBuilder query)
        {
            query.Append("(");
            _left.SetExpression(dialect, query);
            query.Append(" AND ");
            _right.SetExpression(dialect, query);
            query.Append(")");
        }
    }
}