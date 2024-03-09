using System.Linq.Expressions;

namespace RetoApptelinkApi.Helpers
{
    public static class QueryHelper
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string orderByProperty, string sortOrder)
        {
            string command = String.IsNullOrEmpty(sortOrder) || sortOrder.ToLower() == "asc" ? "OrderBy" : "OrderByDescending";
            var type = typeof(T);
            var property = type.GetProperty(orderByProperty);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<T>(resultExpression);
        }

        public static IQueryable<T> FilterBy<T>(this IQueryable<T> source, string filterByProperty, string filterValue)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, filterByProperty);
            var value = Expression.Constant(filterValue);
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

            // Intenta convertir filterValue a un entero si filterByProperty es un entero.
            if (int.TryParse(filterValue, out int intValue) && property.Type == typeof(int))
            {
                value = Expression.Constant(intValue);
            }

            var body = Expression.Call(property, containsMethod, value);
            var predicate = Expression.Lambda<Func<T, bool>>(body, parameter);

            return source.Where(predicate);
        }
    }

}
