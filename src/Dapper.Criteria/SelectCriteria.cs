using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using Dapper.Contrib.Extensions;
using Dapper.Criteria.Attributes;
using Dapper.Criteria.Expressions;
using Dapper.Criteria.Orders;
using Dapper.Criteria.Resolvers;
using Dapper.Criteria.Selects;

namespace Dapper.Criteria
{
    public class SelectCriteria
    {
        public static SelectCriteria Create(string table) 
            => new SelectCriteria(table);

        public static SelectCriteria Create(string table, string schema) 
            => new SelectCriteria(table, schema);

        public static SelectCriteria Create(Type type)
        {
            var table = AttributeResolvers.GetTableName(type);
            var schema = AttributeResolvers.GetSchemaName(type);
            
            
            
            return new 
        }
        
        private int _limit;

        private readonly string _schema;
        private readonly string _table;

        private Queue<IExpression> _filter;
        private readonly Queue<ISelect> _selects = new Queue<ISelect>();
        private readonly Queue<IExpression> _where = new Queue<IExpression>();
        private readonly Queue<IOrder> _orders = new Queue<IOrder>();

        public SelectCriteria(string table)
        {
            _table = table ?? throw new ArgumentNullException(nameof(table));
            _schema = null;
        }
        
        public SelectCriteria(string table, Queue<ISelect> selects)
        {
            _table = table ?? throw new ArgumentNullException(nameof(table));
            _selects = selects ?? throw new ArgumentNullException(nameof(selects));
            _schema = null;
        }

        public SelectCriteria(string table, string schema)
        {
            _table = table ?? throw new ArgumentNullException(nameof(table));
            _schema = schema ?? throw new ArgumentNullException(nameof(schema));
        }
        
        public SelectCriteria(string table, string schema, Queue<ISelect> selects)
        {
            _table = table ?? throw new ArgumentNullException(nameof(table));
            _schema = schema ?? throw new ArgumentNullException(nameof(schema));
            _selects = selects ?? throw new ArgumentNullException(nameof(selects));
        }

        public SelectCriteria Select()
        {
            return this;
        }
        
        public SelectCriteria SetMaxResult(int maxResult)
        {
            _limit = maxResult;
            return this;
        }

        public SelectCriteria SelectColumn(ISelect @select)
        {
            _selects.Enqueue(@select);
            return this;
        }
        
        public SelectCriteria Where(IExpression expression)
        {
            _filter = _where;
            _where.Enqueue(expression);
            return this;
        }

        public SelectCriteria And(IExpression expression)
        {
            _filter.Enqueue(expression);
            return this;
        }

        public SelectCriteria Or(IExpression expression)
        {
            _filter.Enqueue(expression);
            return this;
        }

        public SelectCriteria OrderBy(IOrder order)
        {
            _orders.Enqueue(order);
            return this;
        }

        public string ToRawSql(ISqlDialect dialect)
        {
            var sb = new StringBuilder();

            sb.Append("SELECT ");

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
                    sb.Append(where.ToSql(dialect));
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
            
            return sb.ToString();
        }
    }
}