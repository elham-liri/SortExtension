using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;

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
            bool descendingSort = false;

            var tType = typeof(T);

            if (tType.HasDefaultSortProperty())
            {
                sortProperty = tType.GetDefaultSortProperty();
                descendingSort = tType.IsDefaultSortDirectionDescending();
            }

            var validationError = source.ValidationBeforeSort(sortProperty);
            if (!string.IsNullOrWhiteSpace(validationError))
            {
                throw new ArgumentException(validationError);
            }

            return source.Sort(sortProperty, descendingSort);
        }

        /// <summary>
        /// Sort A collection by a Default sort property and a default sort direction
        /// </summary>
        /// <typeparam name="T">type of collection's objects</typeparam>
        /// <param name="source">the collection</param>
        /// <returns>sorted collection</returns>
        /// <exception cref="ArgumentException">thrown exception</exception>
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> source) where T : class
        {
            string sortProperty = string.Empty;
            bool descendingSort = false;

            var tType = typeof(T);

            if (tType.HasDefaultSortProperty())
            {
                sortProperty = tType.GetDefaultSortProperty();
                descendingSort = tType.IsDefaultSortDirectionDescending();
            }

            var validationError = source.ValidationBeforeSort(sortProperty);
            if (!string.IsNullOrWhiteSpace(validationError))
            {
                throw new ArgumentException(validationError);
            }

            return source.Sort(sortProperty, descendingSort);
        }

        /// <summary>
        /// Sort A collection by a Default sort property and a given sort direction
        /// </summary>
        /// <typeparam name="T">type of collection's objects</typeparam>
        /// <param name="source">the collection</param>
        /// <param name="descendingSort">Should sort in descending order</param>
        /// <returns>sorted collection</returns>
        /// <exception cref="ArgumentException">thrown exception</exception>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, bool descendingSort)
            where T : class
        {
            string sortProperty = string.Empty;

            var tType = typeof(T);

            if (tType.HasDefaultSortProperty())
            {
                sortProperty = tType.GetDefaultSortProperty();
            }

            var validationError = source.ValidationBeforeSort(sortProperty);
            if (!string.IsNullOrWhiteSpace(validationError))
            {
                throw new ArgumentException(validationError);
            }

            return source.Sort(sortProperty, descendingSort);
        }

        /// <summary>
        /// Sort A collection by a Default sort property and a given sort direction
        /// </summary>
        /// <typeparam name="T">type of collection's objects</typeparam>
        /// <param name="source">the collection</param>
        /// <param name="descendingSort">Should sort in descending order</param>
        /// <returns>sorted collection</returns>
        /// <exception cref="ArgumentException">thrown exception</exception>
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> source, bool descendingSort)
            where T : class
        {
            string sortProperty = string.Empty;

            var tType = typeof(T);

            if (tType.HasDefaultSortProperty())
            {
                sortProperty = tType.GetDefaultSortProperty();
            }

            var validationError = source.ValidationBeforeSort(sortProperty);
            if (!string.IsNullOrWhiteSpace(validationError))
            {
                throw new ArgumentException(validationError);
            }

            return source.Sort(sortProperty, descendingSort);
        }


        /// <summary>
        /// Sort A collection by a given sort property and a given sort direction
        /// </summary>
        /// <typeparam name="T">type of collection's objects</typeparam>
        /// <param name="source">the collection</param>
        /// <param name="sortProperty">property to sort by</param>
        /// <param name="descendingSort">Should sort in descending order</param>
        /// <returns>sorted collection</returns>
        /// <exception cref="ArgumentException">thrown exception</exception>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortProperty
            , bool descendingSort) where T : class
        {
            var validationError = source.ValidationBeforeSort(sortProperty);
            if (!string.IsNullOrWhiteSpace(validationError))
            {
                throw new ArgumentException(validationError);
            }

            return source.Sort(sortProperty, descendingSort);
        }

        /// <summary>
        /// Sort A collection by a given sort property and a given sort direction
        /// </summary>
        /// <typeparam name="T">type of collection's objects</typeparam>
        /// <param name="source">the collection</param>
        /// <param name="sortProperty">property to sort by</param>
        /// <param name="descendingSort">Should sort in descending order</param>
        /// <returns>sorted collection</returns>
        /// <exception cref="ArgumentException">thrown exception</exception>
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> source, string sortProperty
            , bool descendingSort) where T : class
        {
            var validationError = source.ValidationBeforeSort(sortProperty);
            if (!string.IsNullOrWhiteSpace(validationError))
            {
                throw new ArgumentException(validationError);
            }

            return source.Sort(sortProperty, descendingSort);
        }


        private static IQueryable<T> Sort<T>(this IQueryable<T> source, string sortProperty, bool descendingSort)
            where T : class
        {
            var tType = typeof(T);
            var property = tType.GetSortPropertyInfo(sortProperty);

            var funcType = typeof(Func<,>)
                .MakeGenericType(tType, property.PropertyType);

            var lambdaBuilder = typeof(Expression)
                .GetMethods()
                .First(x => x.Name == "Lambda" && x.ContainsGenericParameters && x.GetParameters().Length == 2)
                .MakeGenericMethod(funcType);

            var parameter = Expression.Parameter(tType);
            var propertyExpression = Expression.Property(parameter, property);

            var sortLambda = lambdaBuilder
                .Invoke(null, new object[] { propertyExpression, new[] { parameter } });

            var sortPro = typeof(Queryable)
                .GetMethods()
                .FirstOrDefault(
                    x => x.Name == (descendingSort ? "OrderByDescending" : "OrderBy") &&
                         x.GetParameters().Length == 2);
            if (sortPro != null)
            {
                var sorter = sortPro
                    .MakeGenericMethod(tType, property.PropertyType);

                return (IQueryable<T>)sorter
                    .Invoke(null, new[] { source, sortLambda });
            }

            return source;
        }

        private static IEnumerable<T> Sort<T>(this IEnumerable<T> source, string sortProperty, bool descendingSort)
            where T : class
        {
            var tType = typeof(T);
            var property = tType.GetSortPropertyInfo(sortProperty);

            var selectorParam = Expression.Parameter(typeof(T), "keySelector");
            var sourceParam = Expression.Parameter(typeof(IEnumerable<T>), "source");
            var orderBy = typeof(Enumerable)
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .FirstOrDefault(x => x.Name == (descendingSort ? "OrderByDescending" : "OrderBy") && x.GetParameters().Length == 2);

            if (orderBy == null) return source;


            return
                Expression.Lambda<Func<IEnumerable<T>, IOrderedEnumerable<T>>>
                    (
                        Expression.Call
                        (
                            orderBy.MakeGenericMethod(typeof(T), property.PropertyType),
                            sourceParam,
                            Expression.Lambda
                            (
                                typeof(Func<,>).MakeGenericType(typeof(T), property.PropertyType),
                                Expression.Property(selectorParam, property),
                                selectorParam
                            )
                        ),
                        sourceParam
                    )
                    .Compile()(source);
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
            else if (prop.HasAlternativeSortProperty())
            {
                sortProperty = prop.GetAlternativeSortProperty();
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

        private static string ValidationBeforeSort<T>(this IQueryable<T> source, string sortProperty)
        {
            if (source == null)
                return "source collection is null.";

            if (string.IsNullOrWhiteSpace(sortProperty))
                return "sortProperty is not provided";

            var tType = typeof(T);
            var prop = tType.GetSortPropertyInfo(sortProperty);

            return prop == null
                ? $"No property '{sortProperty}' on type '{tType.Name}'"
                : string.Empty;
        }

        private static string ValidationBeforeSort<T>(this IEnumerable<T> source, string sortProperty)
        {
            if (source == null)
                return "source collection is null.";

            if (string.IsNullOrWhiteSpace(sortProperty))
                return "sortProperty is not provided";

            var tType = typeof(T);
            var prop = tType.GetSortPropertyInfo(sortProperty);

            return prop == null
                ? $"No property '{sortProperty}' on type '{tType.Name}'"
                : string.Empty;
        }
    }
}
