using RPApplication.ServiceContracts;
using RPApplication.Services.Helpers;
using RPApplication.SharedDTO;
using System.ComponentModel.DataAnnotations;

namespace RPApplication.WebAPI.Endpoints.v1
{
    public static class CustomerValueEndpoints
    {
        public static void MapCustomerValueEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/v1/values", GetAll);
            app.MapPost("api/v1/values/create", Create);
        }

        public static async Task<IResult> GetAll(string customerCode,
                                                 ICustomerValueService customerValueService,
                                                 HttpContext context)
        {
            List<CustomerValueDTO> customerValues = await customerValueService.GetAllItems(customerCode);

            return Results.Ok(customerValues);
        }

        public static async Task<IResult> Create(CustomerValueDTO addRequest,
                                                 ICustomerValueService customerValueService)
        {
            if (addRequest == null)
            {
                return Results.Problem("Value data was not provided!");
            }

            if (!ValidationHelper.IsModelValid(addRequest, out List<ValidationResult> errors))
            {
                var errorsMessages = string.Join("|", errors.Select(x => x.ErrorMessage));
                return Results.Problem(errorsMessages);
            }

            CustomerValueDTO? customerValueResponse = await customerValueService.GetByValueAndDate(addRequest.Reg1Value,
                                                                                                        addRequest.RegDate);

            if (customerValueResponse != null)
            {
                string dateString = addRequest.RegDate!.Value.ToString("dd.MM.yyyy HH:ss");
                return Results.Problem($"Value id ({addRequest.Reg1Value},{dateString}) is already taken!");
            }

            customerValueResponse = await customerValueService.CreateItem(addRequest);

            if (customerValueResponse == null)
            {
                return Results.Problem("There was a problem creating customer values. Check backend logs.");
            }

            return Results.Ok(customerValueResponse);
        }
    }
}
