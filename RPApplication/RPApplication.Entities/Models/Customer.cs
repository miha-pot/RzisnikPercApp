using System.ComponentModel.DataAnnotations;

namespace RPApplication.Entities.Models
{
    public class Customer
    {
        [Key]
        public string? ExternalCode { get; set; }

        public string? MpCode { get; set; }

        public string? Name { get; set; }

        public string? Street { get; set; }

        public int SerialNumber { get; set; }

        public List<CustomerValue> Values { get; set; } = [];
    }
}
