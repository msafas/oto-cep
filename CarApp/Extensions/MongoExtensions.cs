using System.Linq.Expressions;
using CarApp.DTOs.Common;

namespace CarApp.Extensions;

public static class MongoExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, SearchDto search)
    {
        queryable = queryable.OrderByDynamic(search.SortColumn, search.SortDesc);

        return queryable.Skip((search.PageIndex - 1) * search.PageSize).Take(search.PageSize);
    }


    private static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> source, string propertyName, bool descending)
    {
        var parameter = Expression.Parameter(typeof(T), "e");
        var property = Expression.Property(parameter, propertyName);
        var lambda = Expression.Lambda(property, parameter);

        var methodName = descending ? "OrderByDescending" : "OrderBy";
        var resultExpression = Expression.Call(
            typeof(Queryable),
            methodName,
            new Type[] { typeof(T), property.Type },
            source.Expression,
            Expression.Quote(lambda)
        );

        return source.Provider.CreateQuery<T>(resultExpression);
    }
}