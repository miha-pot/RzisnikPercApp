using RPApplication.SharedDTO;

namespace RPApplication.ServiceContracts
{
    public interface ICustomerValueService : ICommonService<CustomerValueDTO>
    {
        Task<List<CustomerValueDTO>> GetAllItems(string customerCode);
        Task<CustomerValueDTO?> GetByValueAndDate(double value, DateTime? date);
    }
}
