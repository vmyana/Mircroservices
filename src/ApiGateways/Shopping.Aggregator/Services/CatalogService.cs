using Newtonsoft.Json;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public class CatalogService : ICatalogServicecs
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            var response = await _httpClient.GetAsync("/api/v1/Catalog");
            string jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CatalogModel>>(jsonString);
        }

        public async Task<CatalogModel> GetCatalog(string id)
        {
            var response = await _httpClient.GetAsync($"/api/v1/Catalog/{id}");
            string jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CatalogModel>(jsonString);
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
            var response = await _httpClient.GetAsync($"/api/v1/Catalog/GetProductByCategory/{category}");
            string jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CatalogModel>>(jsonString);
        }
    }
}
