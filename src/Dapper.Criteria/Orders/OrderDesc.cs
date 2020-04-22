using System;

namespace Dapper.Criteria.Orders
{
    public class OrderDesc : IOrder
    {
        private readonly string _alias;
        private readonly string _column;

        public OrderDesc(string column, string @alias)
        {
            _column = column ?? throw new ArgumentNullException(nameof(column));
            _alias = alias;
        }

        public string ToSql(ISqlDialect dialect)
        {
            return $"{dialect.GetAlias(_alias)}{dialect.GetColumn(_column)} DESC";
        }
    }
}