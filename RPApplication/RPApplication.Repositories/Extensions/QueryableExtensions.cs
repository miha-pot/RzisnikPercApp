using System.Linq.Expressions;

namespace RPApplication.Repositories.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query,
                                                     string orderByMember,
                                                     string direction)
        {
            var queryElementTypeParam = Expression.Parameter(typeof(T));
            var memberAccess = Expression.PropertyOrField(queryElementTypeParam, orderByMember);
            var keySelector = Expression.Lambda(memberAccess, queryElementTypeParam);

            var orderBy = direction?.ToLower() == "desc" ? "OrderByDescending" : "OrderBy";

            var methodCall = Expression.Call(
                typeof(Queryable),
                orderBy,
                [typeof(T), memberAccess.Type],
                query.Expression,
                Expression.Quote(keySelector));

            return query.Provider.CreateQuery<T>(methodCall);
        }
    }
}
