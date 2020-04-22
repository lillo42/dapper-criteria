namespace System
{
    public static class ObjectExtensions
    {
        public static object ToRawSql(this object value)
        {
            if (value is string
                || value is DateTime
                || value is DateTimeOffset
                || value is Guid
                || value is TimeSpan
                || value is char)
            {
                
            }

            return value.ToString();
        }
    }
}