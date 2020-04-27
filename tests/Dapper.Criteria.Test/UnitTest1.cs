using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Criteria.Expressions;
using Dapper.Criteria.Orders;
using Dapper.Criteria.Selects;
using Xunit;

namespace Dapper.Criteria.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var criteria = SelectCriteria.From(typeof(Client));

            var sql = criteria.ToRawSql(new FakeDialect());
            
            var criteria2 = SelectCriteria.From(nameof(Client))
                .SelectColumn(Select.Column(nameof(Client.Id)))
                .SelectColumn(Select.Column(nameof(Client.Name)))
                .SelectColumn(Select.Column("birth_data", nameof(Client.BirthDate)))
                .SelectColumn(Select.Column("is_enable", nameof(Client.IsEnable)))
                .Where(Expression.Eq(nameof(Client.Name), "name"))
                    .And(Expression.EqLiteralValue(nameof(Client.IsEnable), true))
                .OrderBy(Order.Async(nameof(Client.Id)));
            
            var sql2 = criteria2.ToRawSql(new FakeDialect());
            
            
            var criteria3 = SelectCriteria.From(typeof(Client))
                .InnerJoin(typeof(Address), null); // A ON A.Id = C.AddressId
        }
        
        public class Client
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime BirthDate { get; set; }
            public bool IsEnable { get; set; }
            
            public int AddressId { get; set; }
            
            public Address Address { get; set; }
        }
        
        public class Address
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        
        public class FakeDialect : ISqlDialect
        {
            public void SetLimit(int limit, StringBuilder query)
            {
                query.Append(" LIMIT ")
                    .Append(limit);
            }

            public string GetParameter(string parameter)
            {
                if (parameter.StartsWith("@"))
                {
                    return parameter;
                }
                
                if (parameter.StartsWith(":"))
                {
                    return parameter;
                }

                return ":" + parameter;
            }

            public string GetColumn(string column)
            {
                return "\"" + column + "\"";
            }

            public string GetTable(string table)
            {
                return "\"" + table + "\"";
            }

            public string GetSchema(string schema)
            {
                if (string.IsNullOrEmpty(schema))
                {
                    return "public";
                }

                return "\"" + schema + "\"" ;
            }

            public string GetRawValue(object value)
            {
                return value switch
                {
                    bool _ => value.ToString()?.ToLower(),
                    string _ => "'" + value + "'",
                    _ => value.ToString()
                };
            }
        }
    }
}
