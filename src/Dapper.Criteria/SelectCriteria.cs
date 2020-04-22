using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using Dapper.Contrib.Extensions;
using Dapper.Criteria.Attributes;
using Dapper.Criteria.Expressions;
using Dapper.Criteria.Expressions.Ands;
using Dapper.Criteria.Expressions.Ors;
using Dapper.Criteria.Orders;
using Dapper.Criteria.Resolvers;
using Dapper.Criteria.Selects;

namespace Dapper.Criteria
{
    public class SelectCriteria
    {
        public static SelectCriteria Create(string table) 
            => new SelectCriteria(table, null);

        public static SelectCriteria Create(string table, string schema) 
            => new SelectCriteria(table, schema);

        public static SelectCriteria Create(Type type)
        {
            var table = AttributeResolvers.GetTableName(type);
            var schema = AttributeResolvers.GetSchemaName(type);

            var properties = AttributeResolvers.GetSelectColumn(type);

            return new SelectCriteria(table, schema, properties);
        }
        
        private int? _limit;

        private readonly string _schema;
        private readonly string _table;

        private List<IExpression> _filter;
        private readonly List<ISelect> _selects = new List<ISelect>();
        private readonly List<IExpression> _where = new List<IExpression>();
        private readonly List<IOrder> _orders = new List<IOrder>();

        public SelectCriteria(string table, string schema)
        {
            _table = table ?? throw new ArgumentNullException(nameof(table));
            _schema = schema;
        }
        
        public SelectCriteria(string table, string schema, List<ISelect> selects)
        {
            _table = table ?? throw new ArgumentNullException(nameof(table));
            _schema = schema;
            _selects = selects ?? throw new ArgumentNullException(nameof(selects));
        }

        
        public SelectCriteria SetMaxResult(int maxResult)
        {
            _limit = maxResult;
            return this;
        }

        public SelectCriteria SelectColumn(ISelect @select)
        {
            _selects.Add(@select);
            return this;
        }
        
        public SelectCriteria Where(IExpression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            
            _filter = _where;
            if (_where.Count == 0)
            {
                _where.Add(expression);
            }
            else
            {
                _where.Add(new AndExpression(expression));
            }
            return this;
        }

        public SelectCriteria And(IExpression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            
            _filter.Add(new AndExpression(expression));
            return this;
        }

        public SelectCriteria Or(IExpression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            
            _filter.Add(new OrExpression(expression));
            return this;
        }

        public SelectCriteria OrderBy(IOrder order)
        {
            _orders.Add(order);
            return this;
        }

        public string ToRawSql(ISqlDialect dialect)
        {
            var sb = new StringBuilder();

            sb.Append("SELECT ");

            if (_limit.HasValue && !dialect.LimitIsInTheEndOfQuery)
            {
                sb.Append(dialect.Limit()).Append(" ").Append(_limit.Value)
                    .Append("");
            }

            var isFirst = true;
            
            foreach (var @select in _selects) 
            {
                if (!isFirst)
                {
                    sb.Append(", ");
                }

                sb.Append(select.ToSql(dialect));
                isFirst = false; 
            }

            sb.Append(" ")
                .Append(dialect.GetSchema(_schema)).Append(".")
                .Append(dialect.GetTable(_table));

            if (_where.Count > 0)
            {
                sb.Append(" WHERE ");
                
                foreach (var @where in _where) 
                {
                    sb.Append(where.ToSql(dialect))
                        .Append(" ");
                }
            }

            if (_orders.Count > 0)
            {
                sb.Append(" ORDER BY ");

                isFirst = true;
                foreach (var @order in _orders)
                {
                    if (!isFirst)
                    {
                        sb.Append(", ");
                    }
                
                    sb.Append(order.ToSql(dialect));
                    isFirst = false;
                }
            }
            
            if (_limit.HasValue && dialect.LimitIsInTheEndOfQuery)
            {
                sb.Append(dialect.Limit()).Append(" ").Append(_limit.Value)
                    .Append("");
            }
            
            return sb.ToString();
        }
    }
}