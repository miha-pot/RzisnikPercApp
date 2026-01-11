using RPApplication.Entities.Models;
using RPApplication.Entities.RequestFeatures;
using RPApplication.RepositoryContracts;
using RPApplication.ServiceContracts;
using RPApplication.ServiceContracts.Mappers;
using RPApplication.Services.Helpers;
using RPApplication.SharedDTO;

namespace RPApplication.Services
{
    /// <summary>
    /// Customer business logic methods.
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerDTO> CreateItem(CustomerDTO? addRequest)
        {
            ArgumentNullException.ThrowIfNull(addRequest);

            ValidationHelper.ModelValidation(addRequest);

            Customer customer = addRequest.ToCustomer();

            await _repository.AddAsync(customer);

            return customer.ToCustomerDTO();
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

        public async Task<PagedList<CustomerDTO>> GetAllItems(RequestParameters parameters)
        {
            var customerEntities = await _repository.GetAllProductsAsync(parameters);

            var customerDTOList = customerEntities.Select(x => x.ToCustomerDTO())
                                                  .AsEnumerable();

            return PagedList<CustomerDTO>.ToPagedList(customerEntities,
                                                           customerDTOList);
        }

        public async Task<CustomerDTO?> GetItemById(string itemId)
        {
            Customer? customer = await _repository.GetByIdAsync(itemId);

            return customer?.ToCustomerDTO();
        }

        public async Task<CustomerDTO> UpdateItem(CustomerDTO? updateRequest)
        {
            ArgumentNullException.ThrowIfNull(updateRequest);

            ValidationHelper.ModelValidation(updateRequest);

            Customer? matchingCustomer = await _repository.GetByIdAsync(updateRequest.ExternalCode!);

            if (matchingCustomer == null)
            {
                throw new ArgumentException($"Given product with id '{updateRequest.ExternalCode}' doesn't exists!");
            }

            matchingCustomer.Street = updateRequest.Street;
            matchingCustomer.MpCode = updateRequest.MpCode;
            matchingCustomer.Name = updateRequest.Name;
            matchingCustomer.SerialNo = updateRequest.SerialNumber;

            await _repository.UpdateAsync(matchingCustomer);

            return matchingCustomer.ToCustomerDTO();
        }
    }
}
