using System;
using System.Text;

namespace Dapper.Criteria.Selects
{
    public class SelectColumn : ISelect
    {
        private readonly string _column;
        private readonly string _propertyName;

        public SelectColumn(string column, string propertyName, string alias)
        {
            _column = column ?? throw new ArgumentNullException(nameof(column));
            _propertyName = propertyName ?? column;
            Alias = alias;
        }

        public string SetExpression(ISqlDialect dialect) 
            => $"{dialect.GetAlias(Alias)}{dialect.GetColumn(_propertyName)} AS {dialect.GetColumn(_propertyName)}";

        public string Alias { get; set; }
        
        public void SetExpression(ISqlDialect dialect, StringBuilder query)
        {
            query.Append(dialect.GetAlias(Alias))
                .Append(dialect.GetColumn(_column))
                .Append(" AS ")
                .Append(dialect.GetColumn(_propertyName));
        }
    }
}