using System.Linq.Expressions;

namespace Core.Interfaces.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> FindOneAndIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> navigationPropertyPath);
        Task<int> FindAndCountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> GetOneAsync(int id);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task<IEnumerable<TResoult>> FindAndGroupByAsync<TKey, TResoult>(Expression<Func<TEntity, bool>> predicate,
                                Expression<Func<TEntity, TKey>> keySelector, Expression<Func<IGrouping<TKey, TEntity>, TResoult>> selector);
        Task<IEnumerable<TEntity>> FindAndOrderByAsync<TKey, TResoult>(Expression<Func<TEntity, bool>> predicate,
                                Expression<Func<TEntity, TKey>> keySelector);
        Task<IEnumerable<TEntity>> FindAndIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> navigationPropertyPath);
        Task<IEnumerable<TEntity>> FindAndIncludeX2Async<TProperty, TPropertyX2>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> navigationPropertyPath, Expression<Func<TEntity, TPropertyX2>> navigationPropertyPathX2);
        Task<TEntity?> FindOneAndIncludeX2Async<TProperty, TPropertyX2>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> navigationPropertyPath, Expression<Func<TEntity, TPropertyX2>> navigationPropertyPathX2);

    }
}
