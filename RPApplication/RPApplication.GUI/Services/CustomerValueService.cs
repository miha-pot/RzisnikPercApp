using RPApplication.WebGUI.DTOs.CustomerValueDTO;
using RPApplication.WebGUI.ServiceContracts;
using System.Net;

namespace RPApplication.WebGUI.Services
{
    public class CustomerValueService : ICustomerValueService
    {
        private readonly HttpClient _httpClient;

        public CustomerValueService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CustomerValueResponse>> GetCustomerValues(string customerCode)
        {
            var response = await _httpClient.GetAsync($"values?customerCode={customerCode}");

            if (!response.IsSuccessStatusCode)
            {
                return [];
            }

            var result = await response.Content.ReadFromJsonAsync<List<CustomerValueResponse>>();

            return result ?? [];
        }

        public async Task<string[]?> Create(CustomerValueAddRequest addRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("values/create", addRequest);

            if (response.IsSuccessStatusCode)
            {
                return null;
            }

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                var error = await response.Content.ReadAsStringAsync();
                return [error];
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
    }
}
