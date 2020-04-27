using System;
using System.Text;

namespace Dapper.Criteria.Expressions.Join
{
    public class InnerJoin : IJoin
    {
        public string Alias { get; set; }

        private readonly string _table;
        private readonly string _schema;
        private readonly IExpression _expression;

        public InnerJoin(string schema, string table, string @alias, IExpression expression)
        {
            _table = table ?? throw new ArgumentNullException(nameof(table));
            _schema = schema;
            Alias = alias ?? throw new ArgumentNullException(nameof(alias));
            _expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }


        public void SetExpression(ISqlDialect dialect, StringBuilder query)
        {
            query.Append("INNER JOIN ")
                .Append(dialect.GetSchema(_schema))
                .Append(".")
                .Append(dialect.GetColumn(_table))
                .Append(" ")
                .Append(Alias);
            
            _expression.SetExpression(dialect, query);
        }
    }
}