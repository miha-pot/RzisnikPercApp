using RPApplication.Entities.DatabaseContext;
using RPApplication.Entities.Models;
using RPApplication.Entities.RequestFeatures;
using RPApplication.Repositories.Extensions;
using RPApplication.RepositoryContracts;

namespace RPApplication.Repositories
{
    /// <summary>
    /// Customer specific SQL operation methods.
    /// </summary>
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext db) : base(db)
        {

        }        

        public async Task<PagedList<Customer>> GetAllProductsAsync(RequestParameters parameters)
        {
            var items = _db.Customers.ToList();

            var query = _db.Customers!.AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                var lowerCaseTerm = parameters.SearchTerm.ToLower();
                query = query.Where(x =>
                (x.Name!.ToLower().Contains(lowerCaseTerm) ||
                x.MpCode!.ToLower().Contains(lowerCaseTerm) ||
                x.SerialNo!.ToString().Contains(lowerCaseTerm)));
            }

            if (!string.IsNullOrWhiteSpace(parameters.SortColumn))
            {
                query = query.OrderByDynamic(parameters.SortColumn, parameters.SortOrder!);
            }
            else
            {
                query.OrderBy(x => x.Name);
            }

            //query = query.Include(x => x.Ratings).AsQueryable();

            return await PagedList<Customer>.CreateAsync(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
