namespace Dapper.Criteria
{
    public static class ISqlDialectExtensions
    {
        public static string GetAlias(this ISqlDialect dialect, string alias)
        {
            if (string.IsNullOrEmpty(alias))
            {
                return string.Empty;
            }

            return $"{alias}.";
        }
    }
}