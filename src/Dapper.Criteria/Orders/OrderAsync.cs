using System;
using System.Text;

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
        
        public void SetExpression(ISqlDialect dialect, StringBuilder query)
        {
            query.Append(dialect.GetAlias(Alias))
                .Append(dialect.GetColumn(_column))
                .Append(" ASC");
        }
    }
}