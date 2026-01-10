using RPApplication.WebGUI.DTOs;
using RPApplication.WebGUI.DTOs.CustomerDTO;
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

        public async Task<List<CustomerResponse>> GetCustomers(RequestParameters parameters)
        {
            var response = await _httpClient.PostAsJsonAsync("customers", parameters);

            if (!response.IsSuccessStatusCode)
            {
                return [];
            }

            var result = await response.Content.ReadFromJsonAsync<List<CustomerResponse>>();

            return result ?? [];
        }

        public async Task<string[]?> Create(CustomerAddRequest addRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("customers/create", addRequest);

            if (response.IsSuccessStatusCode)
            {
                return null;
            }

            try
            {
                return await response.Content.ReadFromJsonAsync<string[]>();
            }
            catch
            {
                var error = await response.Content.ReadAsStringAsync();
                return ["An unexpected error occurred.", error];
            }
        }

        public async Task<string> Delete(string externalCode)
        {
            var response = await _httpClient.DeleteAsync($"customers/delete?customerExternalCode={externalCode}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            // Handle failure
            throw new Exception($"Failed to delete. Status: {response.StatusCode}");
        }

        public async Task<string[]?> Edit(CustomerUpdateRequest updateRequest)
        {
            var response = await _httpClient.PutAsJsonAsync("customers/update", updateRequest);

            try
            {
                return await response.Content.ReadFromJsonAsync<string[]>();
            }
            catch
            {
                var error = await response.Content.ReadAsStringAsync();
                return ["An unexpected error occurred.", error];
            }

        }

        public async Task<CustomerResponse?> GetCustomerById(string customerExternalCode)
        {
            var response = await _httpClient.GetAsync("customers/details?customerId=" + customerExternalCode);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<CustomerResponse?>();
        }
    }
}
