using RPApplication.ServiceContracts.DTO.CustomerValueDTO;

namespace RPApplication.ServiceContracts
{
    public interface ICustomerValueService : ICommonService<CustomerValueAddRequest, CustomerValueResponse, CustomerValueUpdateRequest>
    {
        Task<List<CustomerValueResponse>> GetAllItems(string customerCode);
        Task<CustomerValueResponse?> GetByValueAndDate(double value, DateTime? date);
    }
}
