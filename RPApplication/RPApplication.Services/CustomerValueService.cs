using RPApplication.Entities.Models;
using RPApplication.RepositoryContracts;
using RPApplication.ServiceContracts;
using RPApplication.ServiceContracts.DTO.CustomerValueDTO;
using RPApplication.Services.Helpers;

namespace RPApplication.Services
{
    public class CustomerValueService : ICustomerValueService
    {
        private readonly ICustomerValueRepository _repository;

        public CustomerValueService(ICustomerValueRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerValueResponse> CreateItem(CustomerValueAddRequest? addRequest)
        {
            ArgumentNullException.ThrowIfNull(addRequest);

            ValidationHelper.ModelValidation(addRequest);

            CustomerValue customerValue = addRequest.ToCustomerValue();
            customerValue.RegDate = DateTime.Now;

            await _repository.AddAsync(customerValue);

            return customerValue.ToCustomerValueResponse();
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

        public async Task<List<CustomerValueResponse>> GetAllItems(string customerCode)
        {
            List<CustomerValue> customerValueList = await _repository.GetCustomerValues(customerCode);

            return customerValueList.Select(x => x.ToCustomerValueResponse()).ToList();
        }

        public async Task<CustomerValueResponse?> GetItemById(string itemId)
        {
            CustomerValue? customerValue = await _repository.GetByIdAsync(itemId);

            return customerValue?.ToCustomerValueResponse();
        }

        public async Task<CustomerValueResponse> UpdateItem(CustomerValueUpdateRequest? updateRequest)
        {
            ArgumentNullException.ThrowIfNull(updateRequest);

            ValidationHelper.ModelValidation(updateRequest);

            CustomerValue? matchingCustomerValue = await _repository.GetByIdAsync(updateRequest.Reg1Value.ToString() + "" + updateRequest.RegDate.ToString());

            if (matchingCustomerValue == null)
            {
                throw new ArgumentException($"Given order with id {updateRequest.Reg1Value + updateRequest.RegDate.ToString()} doesn't exists!");
            }

            matchingCustomerValue.ValueTypeDescription = updateRequest.ValueTypeDescription;
            matchingCustomerValue.ValueTypeUnit = updateRequest.ValueTypeUnit;

            await _repository.UpdateAsync(matchingCustomerValue);

            return matchingCustomerValue.ToCustomerValueResponse();
        }
    }
}
