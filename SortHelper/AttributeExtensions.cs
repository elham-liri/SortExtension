using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SortHelper.Attributes;
using SortHelper.Enums;

namespace SortHelper
{
    internal static class AttributeExtensions
    {
        internal static bool HasDefaultSortProperty(this Type type)
        {
            return type.HasAttribute<DefaultSortProperty>();
        }

        internal static string GetDefaultSortProperty(this Type type)
        {
            return type.GetPropertyByAttribute<DefaultSortProperty>()?.Name;
        }

        internal static SortDirection? GetDefaultSortDirection(this Type type)
        {
            var property = type.GetPropertyByAttribute<DefaultSortProperty>();
            var attribute
                = Attribute.GetCustomAttribute(property, typeof(DefaultSortProperty))
                    as DefaultSortProperty;

            return attribute?.DefaultSortDirection;
        }

        internal static string GetAliasSortProperty(this Type type, string alias)
        {
            var propertiesWithAlias = type.GetPropertiesByAttribute<AliasSortProperty>().ToList();

            foreach (var property in propertiesWithAlias)
            {
                if (Attribute.GetCustomAttribute(property, typeof(AliasSortProperty)) is AliasSortProperty attribute 
                    && attribute.AliasName == alias) 
                    return property.Name;
            }

            return string.Empty;
        }

        internal static bool HasAlternativeSortProperty(this PropertyInfo property)
        {
            return Attribute.GetCustomAttribute(property, typeof(AlternativeSortProperty)) is AlternativeSortProperty;
        }

        internal static string GetAlternativeSortProperty(this PropertyInfo property)
        {
            var attribute
                = Attribute.GetCustomAttribute(property, typeof(AlternativeSortProperty))
                    as AlternativeSortProperty;

            return attribute?.AlternativeName;
        }

        private static bool HasAttribute<TAttribute>(this Type type) where TAttribute : Attribute
        {
            return type.GetProperties().Any(p => Attribute.IsDefined(p, typeof(TAttribute)));
        }

        private static PropertyInfo GetPropertyByAttribute<TAttribute>(this Type type)
            where TAttribute : Attribute
        {
            var property = type.GetProperties().FirstOrDefault(p => Attribute.IsDefined(p, typeof(TAttribute)));
            return property;
        }

        private static IEnumerable<PropertyInfo> GetPropertiesByAttribute<TAttribute>(this Type type)
            where TAttribute : Attribute
        {
            var properties = type.GetProperties().Where(p => Attribute.IsDefined(p, typeof(TAttribute)));
            return properties;
        }
    }
}
