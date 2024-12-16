using System;
using System.Linq.Expressions;
using System.Linq;
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
            string sortProperty=string.Empty;
            SortDirection? sortDirection=null;

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

            return source.OrderBy(sortProperty, sortDirection.Value);
        }

        private static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortProperty,
            SortDirection sortDirection)
            where T : class
        {
            var tType = typeof(T);
            var prop = tType.GetProperty(sortProperty.FirstCharToUpper());

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
            var prop = tType.GetProperty(sortProperty.FirstCharToUpper());

            if (prop == null)
                return $"No property '{sortProperty}' on type '{tType.Name}'";


            return string.Empty;
        }
    }
}
