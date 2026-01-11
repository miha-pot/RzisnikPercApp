using RPApplication.Entities.Models;
using RPApplication.SharedDTO;

namespace RPApplication.ServiceContracts.Mappers
{
    public static class CustomerMapper
    {
        public static Customer ToCustomer(this CustomerDTO customerDTO)
        {
            return new Customer()
            {
                ExternalCode = customerDTO.ExternalCode,
                MpCode = customerDTO.MpCode,
                Name = customerDTO.Name,
                SerialNo = customerDTO.SerialNumber,
                Street = customerDTO.Street
            };
        }

        public static CustomerDTO ToCustomerDTO(this Customer customer)
        {
            return new CustomerDTO()
            {
                ExternalCode = customer.ExternalCode,
                MpCode = customer.MpCode,
                Name = customer.Name,
                Street = customer.Street,
                SerialNumber = customer.SerialNo,
            };
        }
    }
}
