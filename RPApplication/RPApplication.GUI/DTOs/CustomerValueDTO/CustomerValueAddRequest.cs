using System.ComponentModel.DataAnnotations;

namespace RPApplication.WebGUI.DTOs.CustomerValueDTO
{
    public class CustomerValueAddRequest
    {
        [Required(ErrorMessage = "Reg value is required!")]
        public double Reg1Value { get; set; }

        [Required(ErrorMessage = "Reg date is required!")]
        public DateTime? RegDate { get; set; }

        [Required(ErrorMessage = "Customer external code is required!")]
        public string? CustomerCode { get; set; }

        [Required(ErrorMessage = "Value type description is required!")]
        public string? ValueTypeDescription { get; set; }

        [Required(ErrorMessage = "Value type unit is required!")]
        public string? ValueTypeUnit { get; set; }
    }
}
