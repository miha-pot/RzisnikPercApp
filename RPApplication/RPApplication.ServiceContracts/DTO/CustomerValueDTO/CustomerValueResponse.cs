using RPApplication.Entities.Models;

namespace RPApplication.ServiceContracts.DTO.CustomerValueDTO
{
    public class CustomerValueResponse
    {
        public double Reg1Value { get; set; }

        public DateTime? RegDate { get; set; }

        public string? CustomerCode { get; set; }

        public string? ValueTypeDescription { get; set; }

        public string? ValueTypeUnit { get; set; }

        public CustomerValueUpdateRequest ToCustomerValueUpdateRequest()
        {
            return new CustomerValueUpdateRequest()
            {
                Reg1Value = Reg1Value,
                RegDate = RegDate,
                CustomerCode = CustomerCode,
                ValueTypeDescription = ValueTypeDescription,
                ValueTypeUnit = ValueTypeUnit
            };
        }
    }

    public static class CustomerValueResponseExtensions
    {
        public static CustomerValueResponse ToCustomerValueResponse(this CustomerValue customerValue)
        {
            return new CustomerValueResponse()
            {
                Reg1Value = customerValue.Reg1Value,
                RegDate = customerValue.RegDate,
                CustomerCode = customerValue.CustomerCode,
                ValueTypeDescription = customerValue.ValueTypeDescription,
                ValueTypeUnit = customerValue.ValueTypeUnit
            };
        }
    }
}
