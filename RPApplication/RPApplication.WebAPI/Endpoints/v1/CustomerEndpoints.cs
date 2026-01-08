using RPApplication.Entities.RequestFeatures;
using RPApplication.ServiceContracts;
using RPApplication.ServiceContracts.DTO.CustomerDTO;
using RPApplication.Services.Helpers;
using System.ComponentModel.DataAnnotations;

namespace RPApplication.WebAPI.Endpoints.v1
{
    public static class CustomerEndpoints
    {
        public static void MapCustomerEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("api/v1/customers", GetAll);
            app.MapPost("api/v1/customers/create", Create);
            app.MapDelete("api/v1/customers/delete", Delete);
            app.MapGet("api/v1/customers/details", Details);
            app.MapPut("api/v1/customers/update", Update);
        }

        public static async Task<IResult> GetAll(RequestParameters parameters,
                                                 ICustomerService customerService,
                                                 HttpContext context)
        {
            PagedList<CustomerResponse> pagedCustomers = await customerService.GetAllItems(parameters);

            var metaData = new
            {
                pagedCustomers.MetaData.TotalCount,
                pagedCustomers.MetaData.PageSize,
                pagedCustomers.MetaData.CurrentPage,
                pagedCustomers.MetaData.TotalPages,
                pagedCustomers.MetaData.HasNext,
                pagedCustomers.MetaData.HasPrevious
            };

            context.Response.Headers.Append("X-Pagination", System.Text.Json.JsonSerializer.Serialize(metaData));

            return Results.Ok(pagedCustomers);
        }

        public static async Task<IResult> Create(CustomerAddRequest addRequest,
                                                 ICustomerService customerService)
        {
            if (addRequest == null)
            {
                return Results.Problem("Customer data was not provided!");
            }

            if (!ValidationHelper.IsModelValid(addRequest, out List<ValidationResult> errors))
            {
                var errorsMessages = string.Join("|", errors.Select(x => x.ErrorMessage));
                return Results.Problem(errorsMessages);
            }

            CustomerResponse? customerResponse = await customerService.CreateItem(addRequest);

            return Results.Ok(customerResponse);
        }

        public static async Task<IResult> Delete(string customerId,
                                                 ICustomerService customerService)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                return Results.Problem("Customer id was not provided!");
            }

            CustomerResponse? customerResponse = await customerService.GetItemById(customerId);

            if (customerResponse == null)
            {
                return Results.Problem($"Customer with provided id ({customerId}) was not found!");
            }

            bool result = await customerService.DeleteItem(customerId);

            if (!result)
            {
                return Results.Ok("Error while deleting customer. Check backend logs!");
            }

            return Results.Ok("Customer was successfuly deleted");
        }

        public static async Task<IResult> Details(string customerId,
                                                  ICustomerService customerService)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                return Results.Problem("Customer id was not provided!");
            }

            CustomerResponse? customerResponse = await customerService.GetItemById(customerId);

            if (customerResponse == null)
            {
                return Results.Problem($"Customer with provided id ({customerId}) was not found!");
            }

            return Results.Ok(customerResponse);
        }


        public static async Task<IResult> Update(CustomerUpdateRequest? updateRequest,
                                                 ICustomerService customerService)
        {
            if (updateRequest == null)
            {
                return Results.Problem("Customer data was not provided!");
            }

            if (!ValidationHelper.IsModelValid(updateRequest, out List<ValidationResult> errors))
            {
                var errorsMessages = string.Join("|", errors.Select(x => x.ErrorMessage));

                return Results.Problem(errorsMessages);
            }

            CustomerResponse? customerResponse = await customerService.UpdateItem(updateRequest);

            if (customerResponse == null)
            {
                return Results.Problem("There was an error updating customer. Check backend logs!");
            }

            return Results.Ok(customerResponse);
        }
    }
}
