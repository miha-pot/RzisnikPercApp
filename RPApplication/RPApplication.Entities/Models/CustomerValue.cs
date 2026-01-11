namespace RPApplication.Entities.Models
{
    public class CustomerValue
    {
        /// <summary>
        /// Domain model for storing customer values.
        /// </summary>
        public double Reg1Value { get; set; }

        public DateTime? RegDate { get; set; }

        public string? CustomerCode { get; set; }

        public string? ValueTypeDescription { get; set; }

        public string? ValueTypeUnit { get; set; }
    }
}
