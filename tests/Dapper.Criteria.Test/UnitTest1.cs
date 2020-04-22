using System;
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
            var criteria = SelectCriteria.Create(typeof(Client));
            
            var criteria2 = SelectCriteria.Create(nameof(Client))
                .SelectColumn(Select.Column(nameof(Client.Id)))
                .SelectColumn(Select.Column(nameof(Client.Name)))
                .SelectColumn(Select.Column("birth_data", nameof(Client.BirthDate)))
                .SelectColumn(Select.Column("is_enable", nameof(Client.IsEnable)))
                .Where(Expression.Eq(nameof(Client.Name), "name"))
                    .And(Expression.EqLiteralValue(nameof(Client.IsEnable), true))
                    
                .OrderBy(Order.Async(nameof(Client.Id)));
        }
        
        public class Client
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime BirthDate { get; set; }
            public bool IsEnable { get; set; }
        }
    }
}
