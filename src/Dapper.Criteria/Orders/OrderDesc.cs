using System;

namespace Dapper.Criteria.Orders
{
    public class OrderDesc : IOrder
    {
        private readonly string _column;

        public OrderDesc(string column, string @alias)
        {
            _column = column ?? throw new ArgumentNullException(nameof(column));
            Alias = alias;
        }

        public string Alias { get; set; }

        public string ToSql(ISqlDialect dialect) 
            => $"{dialect.GetAlias(Alias)}{dialect.GetColumn(_column)} DESC";
    }
}