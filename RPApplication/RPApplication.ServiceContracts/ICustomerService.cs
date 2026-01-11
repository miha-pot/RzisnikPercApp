using RPApplication.Entities.RequestFeatures;
using RPApplication.SharedDTO;

namespace RPApplication.ServiceContracts
{
    public interface ICustomerService : ICommonService<CustomerDTO>
    {
        Task<PagedList<CustomerDTO>> GetAllItems(RequestParameters parameters);
    }
}
