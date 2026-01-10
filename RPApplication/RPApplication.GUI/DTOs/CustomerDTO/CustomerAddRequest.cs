using System.ComponentModel.DataAnnotations;

namespace RPApplication.WebGUI.DTOs.CustomerDTO
{
    public class CustomerAddRequest
    {
        [Required(ErrorMessage = "Customer external code is required!")]
        public string? ExternalCode { get; set; }

        [Required(ErrorMessage = "Customer mpCode is required!")]
        public string? MpCode { get; set; }

        [Required(ErrorMessage = "Customer name is required!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Customer street is required!")]
        public string? Street { get; set; }

        [Required(ErrorMessage = "Customer serial number is required!")]
        public string? SerialNumber { get; set; }
    }
}
