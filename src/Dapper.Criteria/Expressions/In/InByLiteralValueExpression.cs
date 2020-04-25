using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Criteria.Expressions.In
{
    public class InByLiteralValueExpression : IExpression
    {
        private readonly string _column;
        private readonly IEnumerable<object> _values;

        public InByLiteralValueExpression(string column, IEnumerable<object> value, string @alias)
        {
            _column = column ?? throw new ArgumentNullException(nameof(column));
            _values = value ?? throw new ArgumentNullException(nameof(value));
            Alias = alias;
        }

        public string Alias { get; set; }

        public void SetExpression(ISqlDialect dialect, StringBuilder query)
        {
            query
                .Append(dialect.GetAlias(Alias))
                .Append(dialect.GetColumn(_column))
                .Append(" IN (");

            var isFirst = false;

            foreach (var value in _values)
            {
                if (!isFirst)
                {
                    query.Append(", ");
                }
                
                query.Append(dialect.GetRawValue(value));
                isFirst = true;
            }
            
            query.Append(")");
        }
    }
}