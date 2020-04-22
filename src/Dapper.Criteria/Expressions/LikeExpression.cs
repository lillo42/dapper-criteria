using System;

namespace Dapper.Criteria.Expressions
{
    public class LikeExpression : IExpression
    {
        private readonly string _alias;
        private readonly string _column;
        private readonly string _value;

        public LikeExpression(string column, string value, string alias)
        {
            _column = column ?? throw new ArgumentNullException(nameof(column));
            _value = value ?? throw new ArgumentNullException(nameof(value));
            _alias = alias;
        }

        public string ToSql(ISqlDialect dialect) 
            => $"{dialect.GetAlias(_alias)}.{dialect.GetColumn(_column)} LIKE '{_value}'";
    }
}