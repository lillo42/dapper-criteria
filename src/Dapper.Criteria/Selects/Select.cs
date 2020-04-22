namespace Dapper.Criteria.Selects
{
    public static class Select
    {
        public static ISelect Column(string column)
            => new SelectColumn(column, null, null);
        
        public static ISelect Column(string column, string propertyName)
            => new SelectColumn(column, propertyName, null);
        
        public static ISelect Column(string column, string propertyName, string alias)
            => new SelectColumn(column, propertyName, alias);
    }
}