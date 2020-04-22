using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dapper.Contrib.Extensions;
using Dapper.Criteria.Attributes;

namespace Dapper.Criteria.Resolvers
{
    internal static class AttributeResolvers
    {
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> _tableName =
            new ConcurrentDictionary<RuntimeTypeHandle, string>();

        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> _schemaName =
            new ConcurrentDictionary<RuntimeTypeHandle, string>();

        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<string>> _selectColumn =
            new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<string>>();

        public static string GetTableName(Type type)
        {
            if (_tableName.TryGetValue(type.TypeHandle, out var name))
            {
                return name;
            }

            //NOTE: This as dynamic trick falls back to handle both our own Table-attribute as well as the one in EntityFramework 
            var tableAttrName =
                type.GetCustomAttribute<TableAttribute>(false)?.Name
                ?? (type.GetCustomAttributes(false)
                    .FirstOrDefault(attr => attr.GetType().Name == "TableAttribute") as dynamic)?.Name;

            if (tableAttrName != null)
            {
                name = tableAttrName;
            }
            else
            {
                name = type.Name + "s";
                if (type.IsInterface && name.StartsWith("I"))
                    name = name.Substring(1);
            }

            _tableName[type.TypeHandle] = name;
            return name;
        }

        public static string GetSchemaName(Type type)
        {
            if (_schemaName.TryGetValue(type.TypeHandle, out var name))
            {
                return name;
            }

            //NOTE: This as dynamic trick falls back to handle both our own Table-attribute as well as the one in EntityFramework 
            var schemaAttributeName =
                type.GetCustomAttribute<SchemaAttribute>(false)?.Name
                ?? (type.GetCustomAttributes(false)
                    .FirstOrDefault(attr => attr.GetType().Name == "TableAttribute") as dynamic)?.Name;

            if (schemaAttributeName != null)
            {
                name = schemaAttributeName;
            }

            _tableName[type.TypeHandle] = name;
            return name;
        }

        public static IEnumerable<string> GetCSelectColumn(Type type)
        {
            if (_selectColumn.TryGetValue(type.TypeHandle, out var column))
            {
                return column;
            }
            
            var properties = new List<string>();

            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance ))
            {
                
            }
        }
    }
}