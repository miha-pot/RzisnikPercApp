using RPApplication.Entities.Models;
using RPApplication.SharedDTO;

namespace RPApplication.ServiceContracts.Mappers
{
    public static class CustomerValueMapper
    {
        public static CustomerValue ToCustomerValue(this CustomerValueDTO customerValueDTO)
        {
            return new CustomerValue()
            {
                Reg1Value = customerValueDTO.Reg1Value,
                RegDate = customerValueDTO.RegDate,
                CustomerCode = customerValueDTO.CustomerCode,
                ValueTypeDescription = customerValueDTO.ValueTypeDescription,
                ValueTypeUnit = customerValueDTO.ValueTypeUnit
            };
        }

        public static CustomerValueDTO ToCustomerValueDTO(this CustomerValue customerValue)
        {
            return new CustomerValueDTO()
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
