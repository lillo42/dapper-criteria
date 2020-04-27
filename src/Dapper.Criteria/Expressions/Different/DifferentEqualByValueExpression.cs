using System;
using System.Text;

namespace Dapper.Criteria.Expressions.Different
{
    public class DifferentEqualByValueExpression : IExpression
    {
        private readonly string _column;
        private readonly object _value;

        public DifferentEqualByValueExpression(string column, object value, string @alias)
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
                .Append(" <> ")
                .Append(dialect.GetRawValue(_value));
        }
    }
}