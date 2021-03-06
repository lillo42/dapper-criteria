using System;
using System.Text;

namespace Dapper.Criteria.Expressions.Likes
{
    public class LikeByValueExpression : IExpression
    {
        private readonly string _column;
        private readonly string _value;

        public LikeByValueExpression(string column, string value, string alias)
        {
            _column = column ?? throw new ArgumentNullException(nameof(column));
            _value = value ?? throw new ArgumentNullException(nameof(value));
            Alias = alias;
        }

        public string Alias { get; set; }
       
        public void SetExpression(ISqlDialect dialect, StringBuilder query)
        {
            query.Append(dialect.GetAlias(Alias))
                .Append(dialect.GetColumn(_column))
                .Append(" LIKE ")
                .Append("'").Append(_value).Append("'");
        }
    }
}