using RPApplication.SharedDTO;
using RPApplication.WebGUI.DTOs;

namespace RPApplication.WebGUI.ServiceContracts
{
    public interface ICustomerService
    {
        Task<List<CustomerDTO>> GetCustomers(RequestParameters parameters);
        Task<string[]?> Create(CustomerDTO addRequest);
        Task<string> Delete(string externalCode);
        Task<string[]?> Edit(CustomerDTO updateRequest);
        Task<CustomerDTO?> GetCustomerById(string customerExternalCode);
    }
}
