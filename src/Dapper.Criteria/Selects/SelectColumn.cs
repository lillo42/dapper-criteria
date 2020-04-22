using System;

namespace Dapper.Criteria.Selects
{
    public class SelectColumn : ISelect
    {
        private readonly string _alias;
        private readonly string _column;
        private readonly string _propertyName;

        public SelectColumn(string column, string propertyName, string alias)
        {
            _column = column ?? throw new ArgumentNullException(nameof(column));
            _propertyName = propertyName ?? column;
            _alias = alias;
        }

        public string ToSql(ISqlDialect dialect) 
            => $"{_alias ?? string.Empty}.{dialect.GetColumn(_propertyName)} AS {dialect.GetColumn(_propertyName)}";
    }
}