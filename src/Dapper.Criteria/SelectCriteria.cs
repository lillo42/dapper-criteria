using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Criteria.Expressions;
using Dapper.Criteria.Expressions.Ands;
using Dapper.Criteria.Expressions.Join;
using Dapper.Criteria.Expressions.Or;
using Dapper.Criteria.Orders;
using Dapper.Criteria.Resolvers;
using Dapper.Criteria.Selects;

namespace Dapper.Criteria
{
    public class SelectCriteria
    {
        public static SelectCriteria From(string table, string schema = null, string alias = null) 
            => new SelectCriteria(table, schema, alias);

        public static SelectCriteria From(Type type)
        {
            var table = Resolver.GetTableName(type);
            var schema = Resolver.GetSchemaName(type);

            var properties = Resolver.GetSelectColumn(type);
            var alias = Resolver.GetDefaultAlias(type);

            foreach (var property in properties)
            {
                property.Alias = alias;
            }
            
            return new SelectCriteria(table, schema, alias, properties);
        }
        
        private int? _maxResult;

        private readonly string _schema;
        private readonly string _table;
        private readonly string _alias;

        private List<IExpression> _filter;
        private readonly List<ISelect> _selects = new List<ISelect>();
        private readonly List<IJoin> _joins = new List<IJoin>();
        private readonly List<IExpression> _where = new List<IExpression>();
        private readonly List<IOrder> _orders = new List<IOrder>();

        public SelectCriteria(string table, string schema, string @alias)
        {
            _table = table ?? throw new ArgumentNullException(nameof(table));
            _schema = schema;
            _alias = alias;
        }
        
        public SelectCriteria(string table, string schema, string @alias, List<ISelect> selects)
        {
            _table = table ?? throw new ArgumentNullException(nameof(table));
            _schema = schema;
            _selects = selects ?? throw new ArgumentNullException(nameof(selects));
            _alias = alias;
        }
        
        public SelectCriteria SetMaxResult(int maxResult)
        {
            _maxResult = maxResult;
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

        public SelectCriteria InnerJoin(Type type, IExpression expression)
        {
            var table = Resolver.GetTableName(type);
            var schema = Resolver.GetSchemaName(type);

            var properties = Resolver.GetSelectColumn(type);
            var alias = Resolver.GetDefaultAlias(type);

            foreach (var property in properties)
            {
                property.Alias = alias;
                _selects.Add(property);
            }
            
            _joins.Add(new InnerJoin(schema, table, alias, expression));
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

                @select.SetExpression(dialect, sb);
                isFirst = false; 
            }

            sb.Append(" FROM ")
                .Append(dialect.GetSchema(_schema)).Append(".")
                .Append(dialect.GetTable(_table))
                .Append(" ");

            if (!string.IsNullOrEmpty(_alias))
            {
                sb.Append(_alias).Append(" ");
            }

            foreach (var @join in _joins)
            {
                join.SetExpression(dialect, sb);
            }
            
            if (_where.Count > 0)
            {
                sb.Append("WHERE ");
                
                foreach (var @where in _where) 
                {
                    @where.SetExpression(dialect, sb);
                    sb.Append(" ");
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
                
                    @order.SetExpression(dialect, sb);
                    isFirst = false;
                }
            }

            if (_maxResult.HasValue)
            {
                dialect.SetLimit(_maxResult.Value, sb);
            }
            
            return sb.ToString();
        }
    }
}