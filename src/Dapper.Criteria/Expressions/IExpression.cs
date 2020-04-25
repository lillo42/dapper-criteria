using System.Text;

namespace Dapper.Criteria.Expressions
{
    public interface IExpression
    {
        string Alias { get; set; }
        void SetExpression(ISqlDialect dialect, StringBuilder query);
    }
}