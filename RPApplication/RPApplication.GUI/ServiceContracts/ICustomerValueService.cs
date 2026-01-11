using RPApplication.SharedDTO;

namespace RPApplication.WebGUI.ServiceContracts
{
    public interface ICustomerValueService
    {
        Task<List<CustomerValueDTO>> GetCustomerValues(string customerCode);
        Task<string[]?> Create(CustomerValueDTO addRequest);
    }
}
