using System;

namespace Dapper.Criteria.Expressions.Eqs
{
    public class EqualByValueExpression : IExpression
    {
        private readonly string _column;
        private readonly object _value;

        public EqualByValueExpression(string column, object value, string @alias)
        {
            _column = column ?? throw new ArgumentNullException(nameof(column));
            _value = value ?? throw new ArgumentNullException(nameof(value));
            Alias = alias;
        }

        public string Alias { get; set; }

        public string ToSql(ISqlDialect dialect) 
            => $"{dialect.GetAlias(Alias)}{dialect.GetColumn(_column)} = {dialect.GetRawValue(_value)}";
    }
}