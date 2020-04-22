using Dapper.Criteria.Expressions;

namespace Dapper.Criteria.Selects
{
    public interface ISelect : IExpression
    {
        string Alias { get; set; }
    }
}