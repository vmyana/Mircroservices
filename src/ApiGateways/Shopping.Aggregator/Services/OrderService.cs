using Newtonsoft.Json;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName)
        {
            var response = await _httpClient.GetAsync($"/api/v1/Order/{userName}");
            string jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<OrderResponseModel>>(jsonString);
        }
    }
}
