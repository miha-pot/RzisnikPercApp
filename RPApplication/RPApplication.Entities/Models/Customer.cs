using System.ComponentModel.DataAnnotations;

namespace RPApplication.Entities.Models
{
    /// <summary>
    /// Domain model for storing customer details.
    /// </summary>
    public class Customer
    {
        [Key]
        public string? ExternalCode { get; set; }

        public string? MpCode { get; set; }

        public string? Name { get; set; }

        public string? Street { get; set; }

        public string? SerialNo { get; set; }

        public List<CustomerValue> Values { get; set; } = [];
    }
}
