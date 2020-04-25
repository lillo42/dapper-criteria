using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dapper.Contrib.Extensions;
using Dapper.Criteria.Attributes;
using Dapper.Criteria.Selects;

namespace Dapper.Criteria.Resolvers
{
    internal static class Resolvers
    {
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> _tableName =
            new ConcurrentDictionary<RuntimeTypeHandle, string>();

        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> _schemaName =
            new ConcurrentDictionary<RuntimeTypeHandle, string>();

        private static readonly ConcurrentDictionary<RuntimeTypeHandle, List<ISelect>> _selectColumn =
            new ConcurrentDictionary<RuntimeTypeHandle, List<ISelect>>();
        
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> _defaultAlias = new ConcurrentDictionary<RuntimeTypeHandle, string>();

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
                type.GetCustomAttribute<SchemaAttribute>(false)?.Name;

            if (schemaAttributeName != null)
            {
                name = schemaAttributeName;
            }

            _tableName[type.TypeHandle] = name;
            return name;
        }

        public static List<ISelect> GetSelectColumn(Type type)
        {
            if (_selectColumn.TryGetValue(type.TypeHandle, out var columns))
            {
                return columns;
            }
            
            columns = new List<ISelect>();

            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var propertyType = property.PropertyType;
                if (!propertyType.IsPrimitive
                    && propertyType != typeof(string)
                    && propertyType != typeof(DateTime)
                    && propertyType != typeof(DateTimeOffset)
                    && propertyType != typeof(Guid)
                    && propertyType != typeof(decimal))
                {
                    continue;
                }
                
                var column =
                    type.GetCustomAttribute<ColumnAttribute>(false)?.Name
                    ?? (type.GetCustomAttributes(false)
                        .FirstOrDefault(attr => attr.GetType().Name == "ColumnAttribute") as dynamic)?.Name;

                if (column == null)
                {
                    column = property.Name;
                }
                
                
                columns.Add(new SelectColumn(column, property.Name, null));
            }
            
            _selectColumn[type.TypeHandle] = columns;
            return columns;
        }

        public static string GetDefaultAlias(Type type)
        {
            if (_defaultAlias.TryGetValue(type.TypeHandle, out var alias))
            {
                return alias;
            }
        }
    }
}