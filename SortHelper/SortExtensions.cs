using System;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;
using SortHelper.Enums;

namespace SortHelper
{
    public static class SortExtensions
    {
        /// <summary>
        /// Sort A collection by a Default sort property and a default sort direction
        /// </summary>
        /// <typeparam name="T">type of collection's objects</typeparam>
        /// <param name="source">the collection</param>
        /// <returns>sorted collection</returns>
        /// <exception cref="ArgumentException">thrown exception</exception>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source) where T : class
        {
            string sortProperty = string.Empty;
            SortDirection? sortDirection = null;

            var tType = typeof(T);

            if (tType.HasDefaultSortProperty())
            {
                sortProperty = tType.GetDefaultSortProperty();
                sortDirection = tType.GetDefaultSortDirection();
            }

            var validationError = source.ValidationBeforeSort(sortProperty, sortDirection);
            if (!string.IsNullOrWhiteSpace(validationError))
            {
                throw new ArgumentException(validationError);
            }

            return source.Sort(sortProperty, sortDirection.Value);
        }

        /// <summary>
        /// Sort A collection by a Default sort property and a given sort direction
        /// </summary>
        /// <typeparam name="T">type of collection's objects</typeparam>
        /// <param name="source">the collection</param>
        /// <param name="sortDirection">direction of sort</param>
        /// <returns>sorted collection</returns>
        /// <exception cref="ArgumentException">thrown exception</exception>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, SortDirection sortDirection)
            where T : class
        {
            string sortProperty = string.Empty;

            var tType = typeof(T);

            if (tType.HasDefaultSortProperty())
            {
                sortProperty = tType.GetDefaultSortProperty();
            }

            var validationError = source.ValidationBeforeSort(sortProperty, sortDirection);
            if (!string.IsNullOrWhiteSpace(validationError))
            {
                throw new ArgumentException(validationError);
            }

            return source.Sort(sortProperty, sortDirection);
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortProperty,
            SortDirection sortDirection) where T : class
        {
            var validationError = source.ValidationBeforeSort(sortProperty, sortDirection);
            if (!string.IsNullOrWhiteSpace(validationError))
            {
                throw new ArgumentException(validationError);
            }

            return source.Sort(sortProperty, sortDirection);
        }


        private static IQueryable<T> Sort<T>(this IQueryable<T> source, string sortProperty,
            SortDirection sortDirection)
            where T : class
        {
            var tType = typeof(T);
            var prop = tType.GetSortPropertyInfo(sortProperty);

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
                    x => x.Name == (sortDirection == SortDirection.Descending ? "OrderByDescending" : "OrderBy") &&
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

        private static PropertyInfo GetSortPropertyInfo(this Type type, string sortProperty)
        {
            if (string.IsNullOrWhiteSpace(sortProperty) && type.HasDefaultSortProperty())
            {
                sortProperty = type.GetDefaultSortProperty();
            }

            if (string.IsNullOrWhiteSpace(sortProperty)) return null;

            var prop = type.GetProperty(sortProperty.FirstCharToUpper());

            if (prop == null)
            {
                sortProperty = type.GetAliasSortProperty(sortProperty);
                if (!string.IsNullOrWhiteSpace(sortProperty))
                    prop = type.GetProperty(sortProperty.FirstCharToUpper());
            }
            else if(prop.HasAlternativeSortProperty())
            {
                sortProperty=prop.GetAlternativeSortProperty();
                prop = type.GetProperty(sortProperty.FirstCharToUpper());
            }

            return prop;
        }

        private static string FirstCharToUpper(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            return input.First().ToString().ToUpper() + string.Join("", input.Skip(1));
        }

        private static string ValidationBeforeSort<T>(this IQueryable<T> source, string sortProperty,
            SortDirection? sortDirection)
        {
            if (source == null)
                return "source collection is null.";

            if (string.IsNullOrWhiteSpace(sortProperty) && !sortDirection.HasValue)
                return "sortProperty and SortDirection are not provided";

            if (string.IsNullOrWhiteSpace(sortProperty))
                return "sortProperty is not provided";

            if (!sortDirection.HasValue)
                return "SortDirection is not provided";

            var tType = typeof(T);
            var prop = tType.GetSortPropertyInfo(sortProperty);

            return prop == null 
                ? $"No property '{sortProperty}' on type '{tType.Name}'" 
                : string.Empty;
        }
    }
}
