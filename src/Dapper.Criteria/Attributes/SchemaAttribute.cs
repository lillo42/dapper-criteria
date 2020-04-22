using System;

namespace Dapper.Criteria.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class SchemaAttribute : Attribute
    {
        public SchemaAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}