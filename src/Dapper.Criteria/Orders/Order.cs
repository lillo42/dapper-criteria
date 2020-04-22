namespace Dapper.Criteria.Orders
{
    public static class Order
    {
        public static IOrder Async(string column)
            => new OrderAsync(column, null);
        
        public static IOrder Async(string column, string alias)
            => new OrderAsync(column, alias);
        
        
        public static IOrder Desc(string column)
            => new OrderDesc(column, null);

        public static IOrder Desc(string column, string alias)
            => new OrderDesc(column, alias);
    }
}