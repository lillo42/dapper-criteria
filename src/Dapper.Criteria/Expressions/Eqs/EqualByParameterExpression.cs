using System;

namespace Dapper.Criteria.Expressions.Eqs
{
    public class EqualByParameterExpression : IExpression
    {
        private readonly string _column;
        private readonly string _parameter;

        public EqualByParameterExpression(string column, string parameter, string @alias)
        {
            _column = column ?? throw new ArgumentNullException(nameof(column));
            _parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
            Alias = alias;
        }

        public string Alias { get; set; }

        public string ToSql(ISqlDialect dialect) 
            => $"{dialect.GetAlias(Alias)}{dialect.GetColumn(_column)} = {dialect.GetParameter(_parameter)}";
    }
}