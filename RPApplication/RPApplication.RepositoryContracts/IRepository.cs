namespace RPApplication.RepositoryContracts
{
    public interface IRepository<T>
        where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<T?> GetByIdAsync(string id);
        Task<bool> DeleteAsync(string id);
    }
}
