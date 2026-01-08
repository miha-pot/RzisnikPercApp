using RPApplication.Entities.RequestFeatures;
using RPApplication.ServiceContracts.DTO.CustomerDTO;

namespace RPApplication.ServiceContracts
{
    public interface ICustomerService : ICommonService<CustomerAddRequest, CustomerResponse, CustomerUpdateRequest>
    {
        Task<PagedList<CustomerResponse>> GetAllItems(RequestParameters parameters);
    }
}
