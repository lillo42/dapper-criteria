using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Criteria.Expressions.In
{
    public class InByParametersExpression : IExpression
    {
        private readonly string _column;
        private readonly IEnumerable<string> _parameters;

        public InByParametersExpression(string column, IEnumerable<string> parameters, string @alias)
        {
            _column = column ?? throw new ArgumentNullException(nameof(column));
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
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

            foreach (var parameter in _parameters)
            {
                if (!isFirst)
                {
                    query.Append(", ");
                }
                
                query.Append(dialect.GetParameter(parameter));
                isFirst = true;
            }
            
            query.Append(")");
        }
    }
}