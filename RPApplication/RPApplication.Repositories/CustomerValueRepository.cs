using Microsoft.EntityFrameworkCore;
using RPApplication.Entities.DatabaseContext;
using RPApplication.Entities.Models;
using RPApplication.RepositoryContracts;

namespace RPApplication.Repositories
{
    /// <summary>
    /// Customer value specific SQL operation methods.
    /// </summary>
    public class CustomerValueRepository : Repository<CustomerValue>, ICustomerValueRepository
    {
        public CustomerValueRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<CustomerValue?> GetByValueAndDate(double value, DateTime? date)
        {
            return await _dbSet.FindAsync(value, date);
        }

        public async Task<List<CustomerValue>> GetCustomerValues(string customerCode)
        {
            return await _db.CustomerValues.Where(x => x.CustomerCode!.Equals(customerCode)).ToListAsync();
        }
    }
}
