using RPApplication.Entities.Models;
using RPApplication.Entities.RequestFeatures;
using RPApplication.RepositoryContracts;
using RPApplication.ServiceContracts;
using RPApplication.ServiceContracts.DTO.CustomerDTO;
using RPApplication.Services.Helpers;

namespace RPApplication.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerResponse> CreateItem(CustomerAddRequest? addRequest)
        {
            ArgumentNullException.ThrowIfNull(addRequest);

            ValidationHelper.ModelValidation(addRequest);

            Customer customer = addRequest.ToCustomer();

            await _repository.AddAsync(customer);

            return customer.ToCustomerResponse();
        }

        public async Task<bool> DeleteItem(string itemId)
        {
            Customer? customer = await _repository.GetByIdAsync(itemId);

            if (customer == null)
            {
                return false;
            }

            return await _repository.DeleteAsync(itemId);
        }

        public async Task<PagedList<CustomerResponse>> GetAllItems(RequestParameters parameters)
        {
            var customerEntities = await _repository.GetAllProductsAsync(parameters);

            var customerDTOList = customerEntities.Select(x => x.ToCustomerResponse())
                                                  .AsEnumerable();

            return PagedList<CustomerResponse>.ToPagedList(customerEntities,
                                                           customerDTOList);
        }

        public async Task<CustomerResponse?> GetItemById(string itemId)
        {
            Customer? customer = await _repository.GetByIdAsync(itemId);

            return customer?.ToCustomerResponse();
        }

        public async Task<CustomerResponse> UpdateItem(CustomerUpdateRequest? updateRequest)
        {
            ArgumentNullException.ThrowIfNull(updateRequest);

            ValidationHelper.ModelValidation(updateRequest);

            Customer? customer = await _repository.GetByIdAsync(updateRequest.ExternalCode!);

            if (customer == null)
            {
                throw new ArgumentException($"Given product with id '{updateRequest.ExternalCode}' doesn't exists!");
            }

            await _repository.UpdateAsync(customer);

            return customer.ToCustomerResponse();
        }
    }
}
