using System;
using System.Linq.Expressions;
using System.Linq;
using SortHelper.Attributes;
using SortHelper.Enums;

namespace SortHelper
{
    public static  class SortExtensions
    {
        /// <summary>
        /// Add Sort Expression To A Query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">collection to be sorted</param>
        /// <param name="sortProperty">property to sort by - should be camelCase</param>
        /// <param name="sortDirection">direction of sort</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortProperty, SortDirection sortDirection)
        where T : class
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "source is null.");
            }

            sortProperty = sortProperty.GetSortProperty<T>();

            if (string.IsNullOrWhiteSpace(sortProperty))
            {
                throw new ArgumentException("sortExpression is null or empty.", nameof(sortProperty));
            }


            var isDescending = sortDirection == SortDirection.Descending;
            var tType = typeof(T);
            var prop = tType.GetProperty(sortProperty.FirstCharToUpper());

            if (prop == null)
            {
                throw new ArgumentException($"No property '{sortProperty}' on type '{tType.Name}'");
            }

            var funcType = typeof(Func<,>)
                .MakeGenericType(tType, prop.PropertyType);

            var lambdaBuilder = typeof(Expression)
                .GetMethods()
                .First(x => x.Name == "Lambda" && x.ContainsGenericParameters && x.GetParameters().Length == 2)
                .MakeGenericMethod(funcType);

            var parameter = Expression.Parameter(tType);
            var propExpress = Expression.Property(parameter, prop);

            var sortLambda = lambdaBuilder
                .Invoke(null, new object[] { propExpress, new[] { parameter } });

            var sortPro = typeof(Queryable)
                .GetMethods()
                .FirstOrDefault(
                    x => x.Name == (isDescending ? "OrderByDescending" : "OrderBy") &&
                         x.GetParameters().Length == 2);
            if (sortPro != null)
            {
                var sorter = sortPro
                    .MakeGenericMethod(tType, prop.PropertyType);

                return (IQueryable<T>)sorter
                    .Invoke(null, new[] { source, sortLambda });
            }
            return source;
        }


        private static string GetSortProperty<T>(this string sortProperty) where T : class
        {
            if(!string.IsNullOrEmpty(sortProperty)) return sortProperty;

            if (typeof(T).HasAttribute<DefaultSortProperty>())
            {
                sortProperty = AttributeExtensions.GetPropertyByAttribute<T, DefaultSortProperty>();
            }

            return sortProperty;
        }

        private static string FirstCharToUpper(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            return input.First().ToString().ToUpper() + string.Join("", input.Skip(1));
        }

    }
}
