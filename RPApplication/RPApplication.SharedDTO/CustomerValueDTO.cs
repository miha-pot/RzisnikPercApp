using System.ComponentModel.DataAnnotations;

namespace RPApplication.SharedDTO
{
    public class CustomerValueDTO
    {
        /// <summary>
        /// First part of CustomerValue identifier
        /// </summary>
        [Required(ErrorMessage = "Reg value is required!")]
        public double Reg1Value { get; set; }

        /// <summary>
        /// Second part of CustomerValue identifier
        /// </summary>
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
