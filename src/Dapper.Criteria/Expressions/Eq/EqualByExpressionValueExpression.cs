using System;
using System.Text;

namespace Dapper.Criteria.Expressions.Eqs
{
    public class EqualByExpressionValueExpression : IExpression
    {
        private readonly string _column;
        private readonly IExpression _value;

        public EqualByExpressionValueExpression(string column, IExpression value, string @alias)
        {
            _column = column ?? throw new ArgumentNullException(nameof(column));
            _value = value ?? throw new ArgumentNullException(nameof(value));
            Alias = alias;
        }

        public string Alias { get; set; }

        public void SetExpression(ISqlDialect dialect, StringBuilder query)
        {
            query.Append(dialect.GetAlias(Alias))
                .Append(dialect.GetColumn(_column))
                .Append(" = (");
            
            _value.SetExpression(dialect, query);

            query.Append(")");
        }
    }
}