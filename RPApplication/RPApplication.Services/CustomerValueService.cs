using RPApplication.Entities.Models;
using RPApplication.RepositoryContracts;
using RPApplication.ServiceContracts;
using RPApplication.ServiceContracts.Mappers;
using RPApplication.Services.Helpers;
using RPApplication.SharedDTO;

namespace RPApplication.Services
{
    /// <summary>
    /// Customer value business logic methods.
    /// </summary>
    public class CustomerValueService : ICustomerValueService
    {
        private readonly ICustomerValueRepository _repository;

        public CustomerValueService(ICustomerValueRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerValueDTO> CreateItem(CustomerValueDTO? addRequest)
        {
            ArgumentNullException.ThrowIfNull(addRequest);

            ValidationHelper.ModelValidation(addRequest);

            CustomerValue customerValue = addRequest.ToCustomerValue();

            await _repository.AddAsync(customerValue);

            return customerValue.ToCustomerValueDTO();
        }

        public async Task<bool> DeleteItem(string itemId)
        {
            CustomerValue? customerValue = await _repository.GetByIdAsync(itemId);

            if (customerValue == null)
            {
                return false;
            }

            return await _repository.DeleteAsync(itemId);
        }

        public async Task<List<CustomerValueDTO>> GetAllItems(string customerCode)
        {
            List<CustomerValue> customerValueList = await _repository.GetCustomerValues(customerCode);

            return customerValueList.Select(x => x.ToCustomerValueDTO()).ToList();
        }

        public async Task<CustomerValueDTO?> GetByValueAndDate(double value, DateTime? date)
        {
            CustomerValue? customerValue = await _repository.GetByValueAndDate(value, date);

            return customerValue?.ToCustomerValueDTO();
        }

        public async Task<CustomerValueDTO?> GetItemById(string itemId)
        {
            CustomerValue? customerValue = await _repository.GetByIdAsync(itemId);

            return customerValue?.ToCustomerValueDTO();
        }

        public async Task<CustomerValueDTO> UpdateItem(CustomerValueDTO? updateRequest)
        {
            ArgumentNullException.ThrowIfNull(updateRequest);

            ValidationHelper.ModelValidation(updateRequest);

            string valueId = updateRequest.Reg1Value.ToString() + "" + updateRequest.RegDate.ToString();
            CustomerValue? matchingCustomerValue = await _repository.GetByIdAsync(valueId);

            if (matchingCustomerValue == null)
            {
                throw new ArgumentException($"Given order with id {valueId} doesn't exists!");
            }

            matchingCustomerValue.ValueTypeDescription = updateRequest.ValueTypeDescription;
            matchingCustomerValue.ValueTypeUnit = updateRequest.ValueTypeUnit;

            await _repository.UpdateAsync(matchingCustomerValue);

            return matchingCustomerValue.ToCustomerValueDTO();
        }
    }
}
