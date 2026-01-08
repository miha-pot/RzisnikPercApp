using RPApplication.Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace RPApplication.ServiceContracts.DTO.CustomerDTO
{
    public class CustomerAddRequest
    {
        [Required(ErrorMessage = "Customer mpCode is required!")]
        public string? MpCode { get; set; }

        [Required(ErrorMessage = "Customer name is required!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Customer street is required!")]
        public string? Street { get; set; }

        [Required(ErrorMessage = "Customer serial number is required!")]
        public int SerialNumber { get; set; }

        public Customer ToCustomer()
        {
            return new Customer()
            {
                MpCode = MpCode,
                Name = Name,
                Street = Street,
                SerialNumber = SerialNumber
            };
        }
    }
}
