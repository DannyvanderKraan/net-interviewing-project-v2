using Insurance.Api.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using Microsoft.Extensions.Options;
using Insurance.Api.Settings;
using Microsoft.Extensions.Logging;

namespace Insurance.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly ProductAPISettings _productAPISettings;

        public ProductRepository(ILogger<ProductRepository> logger,
            IOptions<ProductAPISettings> productAPISettings)
        {
            _logger = logger;
            _productAPISettings = productAPISettings.Value;
        }

        /// <inheritdoc/>
        public virtual ProductDto GetProduct(int productID)
        {
            try
            {
                HttpClient client = new HttpClient { BaseAddress = new Uri(_productAPISettings.Url) };
                string json = client.GetAsync(string.Format("/products/{0:G}", productID)).Result.Content.ReadAsStringAsync().Result;
                var product = JsonConvert.DeserializeObject<ProductDto>(json);

                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, default);
                throw;
            }
        }
    }
}
