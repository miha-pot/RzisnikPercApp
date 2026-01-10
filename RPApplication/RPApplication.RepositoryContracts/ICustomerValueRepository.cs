using RPApplication.Entities.Models;

namespace RPApplication.RepositoryContracts
{
    public interface ICustomerValueRepository : IRepository<CustomerValue>
    {
        Task<List<CustomerValue>> GetCustomerValues(string customerCode);
        Task<CustomerValue?> GetByValueAndDate(double value, DateTime? date);
    }
}
