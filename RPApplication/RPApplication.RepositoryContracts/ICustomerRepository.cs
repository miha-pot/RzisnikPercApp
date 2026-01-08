using RPApplication.Entities.Models;
using RPApplication.Entities.RequestFeatures;

namespace RPApplication.RepositoryContracts
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<PagedList<Customer>> GetAllProductsAsync(RequestParameters parameters);
    }
}
