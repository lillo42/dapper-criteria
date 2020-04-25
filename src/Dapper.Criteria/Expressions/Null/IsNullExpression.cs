using System;
using System.Text;

namespace Dapper.Criteria.Expressions.Null
{
    public class IsNullExpression : IExpression
    {
        private readonly string _column;

        public IsNullExpression(string column, string @alias)
        {
            _column = column ?? throw new ArgumentNullException(nameof(column));
            Alias = alias;
        }

        public string Alias { get; set; }

        public void SetExpression(ISqlDialect dialect, StringBuilder query)
        {
            query.Append(dialect.GetAlias(Alias))
                .Append(dialect.GetColumn(_column))
                .Append(" IS NULL");
        }
    }
}