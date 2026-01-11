using Microsoft.AspNetCore.Mvc;
using RPApplication.SharedDTO;
using RPApplication.WebGUI.DTOs;
using RPApplication.WebGUI.ServiceContracts;

namespace RPApplication.WebGUI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CustomerDTO>> GetCustomers(RequestParameters parameters)
        {
            var response = await _httpClient.PostAsJsonAsync("customers", parameters);

            if (!response.IsSuccessStatusCode)
            {
                return [];
            }

            var result = await response.Content.ReadFromJsonAsync<List<CustomerDTO>>();

            return result ?? [];
        }

        public async Task<string[]?> Create(CustomerDTO addRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("customers/create", addRequest);

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

        public async Task<string> Delete(string externalCode)
        {
            var response = await _httpClient.DeleteAsync($"customers/delete?customerExternalCode={externalCode}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return $"Failed to delete. Status: {response.StatusCode}";
        }

        public async Task<string[]?> Edit(CustomerDTO updateRequest)
        {
            var response = await _httpClient.PutAsJsonAsync("customers/update", updateRequest);

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

        public async Task<CustomerDTO?> GetCustomerById(string customerExternalCode)
        {
            var response = await _httpClient.GetAsync("customers/details?customerId=" + customerExternalCode);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<CustomerDTO?>();
        }
    }
}
