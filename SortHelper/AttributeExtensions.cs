﻿using System;
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
    }
}
