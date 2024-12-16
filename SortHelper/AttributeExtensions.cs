using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SortHelper
{
    public static  class AttributeExtensions
    {
        public static bool HasAttribute<T>(this ICustomAttributeProvider provider)
            where T : Attribute
        {
            var attributes = provider.GetCustomAttributes(typeof(T), true);
            return attributes.Length > 0;
        }

        public static string GetPropertyByAttribute<TModel,TAttribute>()
            where TModel : class 
            where TAttribute : Attribute
        {
            var props = typeof(TModel).GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(TAttribute)));

            return props.FirstOrDefault()?.Name;
        }
    }
}
