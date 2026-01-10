using RPApplication.WebGUI.DTOs.CustomerValueDTO;

namespace RPApplication.WebGUI.ServiceContracts
{
    public interface ICustomerValueService
    {
        Task<List<CustomerValueResponse>> GetCustomerValues(string customerCode);
        Task<string[]?> Create(CustomerValueAddRequest addRequest);
    }
}
