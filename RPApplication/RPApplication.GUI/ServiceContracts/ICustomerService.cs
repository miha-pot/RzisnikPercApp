using RPApplication.WebGUI.DTOs;
using RPApplication.WebGUI.DTOs.CustomerDTO;

namespace RPApplication.WebGUI.ServiceContracts
{
    public interface ICustomerService
    {
        Task<List<CustomerResponse>> GetCustomers(RequestParameters parameters);
        Task<string[]?> Create(CustomerAddRequest addRequest);
        Task<string> Delete(string externalCode);
        Task<string[]?> Edit(CustomerUpdateRequest updateRequest);
        Task<CustomerResponse?> GetCustomerById(string customerExternalCode);
    }
}
