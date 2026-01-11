using Microsoft.AspNetCore.Mvc;
using RPApplication.SharedDTO;
using RPApplication.WebGUI.ServiceContracts;

namespace RPApplication.WebGUI.Services
{
    public class CustomerValueService : ICustomerValueService
    {
        private readonly HttpClient _httpClient;

        public CustomerValueService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CustomerValueDTO>> GetCustomerValues(string customerCode)
        {
            var response = await _httpClient.GetAsync($"values?customerCode={customerCode}");

            if (!response.IsSuccessStatusCode)
            {
                return [];
            }

            var result = await response.Content.ReadFromJsonAsync<List<CustomerValueDTO>>();

            return result ?? [];
        }

        public async Task<string[]?> Create(CustomerValueDTO addRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("values/create", addRequest);

            if (response.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();

                string errorMessage = problemDetails?.Detail ?? "An unexpected error occurred.";

                return [errorMessage];
            }
        }
    }
}
