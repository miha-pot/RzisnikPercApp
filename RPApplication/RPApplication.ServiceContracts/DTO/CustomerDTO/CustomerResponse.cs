using RPApplication.Entities.Models;

namespace RPApplication.ServiceContracts.DTO.CustomerDTO
{
    public class CustomerResponse
    {
        public string? ExternalCode { get; set; }

        public string? MpCode { get; set; }

        public string? Name { get; set; }

        public string? Street { get; set; }

        public int SerialNumber { get; set; }

        public CustomerUpdateRequest ToCustomerUpdateRequest()
        {
            return new CustomerUpdateRequest()
            {
                ExternalCode = ExternalCode,
                MpCode = MpCode,
                Name = Name,
                Street = Street,
                SerialNumber = SerialNumber
            };
        }
    }

    public static class CustomerResponseExtensions
    {
        public static CustomerResponse ToCustomerResponse(this Customer customer)
        {
            return new CustomerResponse()
            {
                ExternalCode = customer.ExternalCode,
                MpCode = customer.MpCode,
                Name = customer.Name,
                Street = customer.Street,
                SerialNumber = customer.SerialNumber,
            };
        }
    }
}
