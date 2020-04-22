using System;

namespace Dapper.Criteria.Orders
{
    public class OrderAsync : IOrder
    {
        private readonly string _column;

        public OrderAsync(string column, string @alias)
        {
            _column = column ?? throw new ArgumentNullException(nameof(column));
            Alias = alias;
        }

        public string Alias { get; set; }

        public string ToSql(ISqlDialect dialect) 
            => $"{dialect.GetAlias(Alias)}{dialect.GetColumn(_column)} ASC";
    }
}