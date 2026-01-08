using RPApplication.ServiceContracts;
using RPApplication.ServiceContracts.DTO.CustomerValueDTO;
using RPApplication.Services.Helpers;
using System.ComponentModel.DataAnnotations;

namespace RPApplication.WebAPI.Endpoints.v1
{
    public static class CustomerValueEndpoints
    {
        public static void MapCustomerValueEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/v1/values", GetAll);
            app.MapPost("api/v1/values/create", Create);
            app.MapDelete("api/v1/values/delete", Delete);
            app.MapGet("api/v1/values/details", Details);
            app.MapPut("api/v1/values/update", Update);
        }

        public static async Task<IResult> GetAll(string customerCode,
                                                 ICustomerValueService customerValueService,
                                                 HttpContext context)
        {
            List<CustomerValueResponse> customerValues = await customerValueService.GetAllItems(customerCode);

            return Results.Ok(customerValues);
        }

        public static async Task<IResult> Create(CustomerValueAddRequest addRequest,
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

            CustomerValueResponse? customerResponse = await customerValueService.CreateItem(addRequest);

            return Results.Ok(customerResponse);
        }

        public static async Task<IResult> Delete(string? valueId,
                                                 ICustomerValueService customerValueService)
        {
            if (string.IsNullOrEmpty(valueId))
            {
                return Results.Problem("Value id was not provided!");
            }

            CustomerValueResponse? valueResponse = await customerValueService.GetItemById(valueId);

            if (valueResponse == null)
            {
                return Results.Problem($"Value with provided id ({valueId}) was not found!");
            }

            bool result = await customerValueService.DeleteItem(valueId);

            if (!result)
            {
                return Results.Ok("Error while deleting value. Check backend logs!");
            }

            return Results.Ok("Value was successfuly deleted");
        }

        public static async Task<IResult> Details(string valueId,
                                                  ICustomerValueService customerValueService)
        {
            if (string.IsNullOrEmpty(valueId))
            {
                return Results.Problem("Value id was not provided!");
            }

            CustomerValueResponse? valueResponse = await customerValueService.GetItemById(valueId);

            if (valueResponse == null)
            {
                return Results.Problem($"Value with provided id ({valueId}) was not found!");
            }

            return Results.Ok(valueResponse);
        }


        public static async Task<IResult> Update(CustomerValueUpdateRequest? updateRequest,
                                                 ICustomerValueService customerValueService)
        {
            if (updateRequest == null)
            {
                return Results.Problem("Value data was not provided!");
            }

            if (!ValidationHelper.IsModelValid(updateRequest, out List<ValidationResult> errors))
            {
                var errorsMessages = string.Join("|", errors.Select(x => x.ErrorMessage));

                return Results.Problem(errorsMessages);
            }

            CustomerValueResponse? customerResponse = await customerValueService.UpdateItem(updateRequest);

            if (customerResponse == null)
            {
                return Results.Problem("There was an error updating value. Check backend logs!");
            }

            return Results.Ok(customerResponse);
        }
    }
}
