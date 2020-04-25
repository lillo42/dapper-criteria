using System;
using System.Text;

namespace Dapper.Criteria.Expressions.Greater
{
    public class GreaterOrEqualByParameterExpression : IExpression
    {
        public string Alias { get; set; }

        private readonly string _column;
        private readonly string _parameter;

        public GreaterOrEqualByParameterExpression(string column, string parameter, string @alias)
        {
            _column = column ?? throw new ArgumentNullException(nameof(column));
            _parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
            Alias = @alias;
        }

        public void SetExpression(ISqlDialect dialect, StringBuilder query)
        {
            query
                .Append(dialect.GetAlias(Alias))
                .Append(dialect.GetColumn(_column))
                .Append(" >= ")
                .Append(dialect.GetParameter(_parameter));
        }
    }
}