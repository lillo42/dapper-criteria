using System;

namespace Dapper.Criteria.Orders
{
    public class OrderAsync : IOrder
    {
        private readonly string _alias;
        private readonly string _column;

        public OrderAsync(string column, string @alias)
        {
            _column = column ?? throw new ArgumentNullException(nameof(column));
            _alias = alias;
        }

        public string ToSql(ISqlDialect dialect) 
            => $"{dialect.GetAlias(_alias)}{dialect.GetColumn(_column)} ASC";
    }
}