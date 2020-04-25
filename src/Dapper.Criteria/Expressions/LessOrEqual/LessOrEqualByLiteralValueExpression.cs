using System;
using System.Text;

namespace Dapper.Criteria.Expressions.LessOrEqual
{
    public class LessOrEqualByLiteralValueExpression : IExpression
    {
        public string Alias { get; set; }

        private readonly string _column;
        private readonly object _value;

        public LessOrEqualByLiteralValueExpression(string column, object parameter, string @alias)
        {
            _column = column ?? throw new ArgumentNullException(nameof(column));
            _value = parameter ?? throw new ArgumentNullException(nameof(parameter));
            Alias = @alias;
        }

        public void SetExpression(ISqlDialect dialect, StringBuilder query)
        {
            query
                .Append(dialect.GetAlias(Alias))
                .Append(dialect.GetColumn(_column))
                .Append(" <= ")
                .Append(dialect.GetRawValue(_value));
        }
    }
}